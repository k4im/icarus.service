using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Data;
using icarus.fornecedores.Models;
using icarus.fornecedores.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace icarus.fornecedores.Repository
{
    public class RepoFornecedor : IRepoFornecedor
    {

        readonly DataContext _db;

        public RepoFornecedor(DataContext db)
        {
            _db = db;
        }

        public async Task<ResponseDTO> BuscarFornecedores(int pagina, float resulatdoPorPagina)
        {
            var resultadoPaginas = resulatdoPorPagina;
            var fornecedores = await _db.Fornecedores.ToListAsync();
            var totalDePaginas = Math.Ceiling(fornecedores.Count() / resultadoPaginas);
            var fornecedoresPaginados = fornecedores.Skip((pagina - 1) * (int)resultadoPaginas).Take((int)resultadoPaginas).ToList();

            var response = new ResponseDTO
            {
                Fornecedores = fornecedoresPaginados,
                PaginaAtual = pagina,
                TotalDePaginas = (int)totalDePaginas
            };

            return response;
        }

        public async Task<Fornecedor> BuscarPorId(int? id)
        {
            var fornecedor = await _db.Fornecedores.FirstOrDefaultAsync(x => x.Id == id);
            if(fornecedor == null) Results.NotFound();
            return fornecedor;
        }


        public async Task CriarFornecedor(Fornecedor model)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var fornecedor = new Fornecedor(model.Nome, model.Cnpj, model.Endereco, model.Telefone);
                    await _db.Fornecedores.AddAsync(fornecedor);
                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Não foi possivel estar realizando esta operação: {e.Message}");
                }
            }    
            
        }

        public async Task TrocaDeTelefone(int? id, Telefone model)
        {
            using(var transaction = await _db.Database.BeginTransactionAsync())
            {

                try
                {

                    var fornecedor = await BuscarPorId(id);
                    fornecedor.MudarTelefone(model);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Não foi possivel atualizar o dado, pois o mesmo já foi atualizado por outro usuario!");
                }
                catch(Exception e )
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Não foi possivel realizar está operação: {e.Message}");

                }
            }
            
        }
        public async Task TrocaDeEndereco(int? id, Endereco model)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {   
                try
                {
                    var fornecedor = await BuscarPorId(id);
                    fornecedor.MudancaDeEndereco(model);
                    await _db.SaveChangesAsync();
                    
                    await transaction.CommitAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Não foi possivel atualizar o dado, pois o mesmo já foi atualizado por outro usuario!");
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Não foi possivel realizar está operação: {e.Message}");
                }
            }
        }


        public async Task DeletarFornecedor(int? id)
        {
            using(var transaction = await _db.Database.BeginTransactionAsync())
            {

                try
                {
                    var fornecedor = await BuscarPorId(id);
                    _db.Remove(fornecedor);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Não foi possivel deletar o dado, tente mais tarde!");
                }
                catch(Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Não foi possivel possivel realizar esta operação: {e.Message}");
                }

            }
            
            
        }
    }
}