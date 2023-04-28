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
            var item = await BuscarProdutoId(id);
            item.Quantidade = modelo.Quantidade;
            _db.Produtos.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task<Produto> BuscarProdutoId(int? id)
        {
            var item = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public async Task<EstoqueResponseDTO> BuscarProdutos(int pagina)
        {
            var ResultadoPorPagina = 5f;
            var projetos = await _db.Produtos.ToListAsync();
            var TotalDePaginas = Math.Ceiling(projetos.Count() / ResultadoPorPagina);
            var projetosPaginados = projetos.Skip((pagina - 1) * (int)ResultadoPorPagina).Take((int)ResultadoPorPagina).ToList();
            
            var response = new EstoqueResponseDTO {
                Projects = projetosPaginados,
                Paginas = (int)TotalDePaginas,
                PaginaAtual = pagina,
                TotalDePaginas = (int)TotalDePaginas 
            };
            return response;
        }

        public async Task DeletarProduto(int? id)
        {
            var produto = await BuscarProdutoId(id);
            _db.Produtos.Remove(produto);
            await _db.SaveChangesAsync();
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
    
    
        public async Task TratarMessage(int QuantidadeDeChapa, int id)
        {
            var produto =  await _db.Produtos.FirstOrDefaultAsync(x => x.Nome.ToLower().Contains("chapa"));
            if(produto == null) Results.NotFound();
            produto.Quantidade -= QuantidadeDeChapa;
            _db.Produtos.Update(produto);
            await _db.SaveChangesAsync();
        }

    }
}