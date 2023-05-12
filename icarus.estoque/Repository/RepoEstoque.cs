using icarus.estoque.Data;
using icarus.estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace icarus.estoque.Repository
{
    public class RepoEstoque : IRepoEstoque
    {

        private readonly DataContextEstoque _db;

        public RepoEstoque(DataContextEstoque db)
        {
            _db = db;
        }

        public async Task AtualizarProduto(int? id, Produto modelo)
        {
            try
            {
                await _db.Produtos.Where(produto => produto.Id == id)
                    .ExecuteUpdateAsync(updates =>
                    updates.SetProperty(produto => produto.Quantidade, modelo.Quantidade));
            }
            catch (DbUpdateConcurrencyException)
            {

                Console.WriteLine("Não foi possivel realizar a operação, por favor tente mais tarde!");
            }
        }

        public async Task<Produto> BuscarProdutoId(int? id)
        {
            var item = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<EstoqueResponseDTO> BuscarProdutos(int pagina, float resultadoPorPagina)
        {
            var ResultadoPorPagina = resultadoPorPagina;
            var projetos = await _db.Produtos.ToListAsync();
            
            var TotalDePaginas = Math.Ceiling(projetos.Count() / ResultadoPorPagina);
            var projetosPaginados = projetos.Skip((pagina - 1) * (int)ResultadoPorPagina).Take((int)ResultadoPorPagina).ToList();
            
            var response = new EstoqueResponseDTO {
                Produtos = projetosPaginados,
                Paginas = (int)TotalDePaginas,
                PaginaAtual = pagina,
                TotalDePaginas = (int)TotalDePaginas 
            };
            return response;
        }

        public async Task DeletarProduto(int? id)
        {
            await _db.Produtos.Where(produto => produto.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task NovoProduto(Produto modelo)
        {
            var produto = new Produto
            {
                Id = modelo.Id,
                Nome = modelo.Nome,
                Cor = modelo.Cor,
                Quantidade = modelo.Quantidade,
                ValorUnitario = modelo.ValorUnitario
            };
            await _db.Produtos.AddAsync(produto);
            await _db.SaveChangesAsync();
        }
    
    
        public async Task TratarMessage(ConsumerDTO consumer)
        {
            foreach (var projeto in consumer.Projetos)
            {
                var produto =  await _db.Produtos.FirstOrDefaultAsync(x => x.Nome.ToLower() == projeto.Chapa);
                if(produto == null) Results.NotFound();
                produto.Quantidade -= projeto.QuantidadeDeChapa;
                _db.Produtos.Update(produto);
                await _db.SaveChangesAsync();
            }
        }

    
    
        public async Task ValidarProdutos()
        {
            var produtos = await _db.Produtos.ToListAsync();
            foreach (var produto in produtos)
            {
                if(produto.Quantidade <= 0) await DeletarProduto(produto.Id);
            }
        }

    }
}