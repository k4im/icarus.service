using icarus.estoque.Data;
using icarus.estoque.Models;
using icarus.estoque.Models.ValueObjects;
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

        public async Task EntradaDeProdutos(int? id, Quantidade quantidade)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var produto = await BuscarProdutoId(id);
                    produto.EntradaDeProduto(quantidade.QuantidadeItem);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("Não foi possivel atualizar o dado pois alguém já realizou esta operação");
                }
                catch(Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Não foi possivel realizar esta operação: {e.Message}");
                }
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
            using(var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var produto = await BuscarProdutoId(id);
                    _db.Remove(produto);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("Não foi possivel realizar esta operação pois a mesma já foi realizada por outro usuario!");
                }
                catch(Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Não foi possivel realizar a operação: {e.Message}");
                }
            }
        }

        public async Task NovoProduto(Produto modelo)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var produto = new Produto(modelo.Nome, modelo.Cor, modelo.QuantidadeProduto, modelo.ValorUnitario);
                    await _db.AddAsync(produto);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("Não foi possivel realizar a ação, outro usuario realizou a mesma!");
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Não foi possivel realizar a ação: {e.Message}");
                }
            }
        }
    
    
        public async Task TratarMessage(ConsumerDTO consumer)
        {
            using(var transaction = await _db.Database.BeginTransactionAsync())
            {
                foreach (var projeto in consumer.Projetos)
                {
                        try
                        {
                            var produto =  await _db.Produtos.FirstOrDefaultAsync(x => x.Nome.ToLower() == projeto.Chapa);
                            if(produto == null) Results.NotFound();
                            produto.SaidaDeProduto(projeto.QuantidadeDeChapa);
                            await _db.SaveChangesAsync();
                            await transaction.CommitAsync();
                        }
                        catch (Exception e)
                        {
                            await transaction.RollbackAsync();
                            Console.WriteLine($"Não foi possivel realizar a operação de tratativa de mensagem: {e.Message}");
                        }
                }
            }
        }

        public async Task ValidarProdutos()
        {
            var produtos = await _db.Produtos.ToListAsync();
            foreach (var produto in produtos)
            {
                if(produto.QuantidadeProduto.QuantidadeItem <= 0) await DeletarProduto(produto.Id);
            }
        }

    }
}