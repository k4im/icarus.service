using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Data;
using icarus.fornecedores.Models;
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
            // var fornecedor = new Fornecedor(model.Nome, model.Cnpj, model.Endereço, model.Telefone);
            // await _db.Fornecedores.AddAsync(fornecedor);
            await _db.SaveChangesAsync();
        }

        public async Task AtualizarFornecedor(int? id, Fornecedor model)
        {
            var fornecedor = await BuscarPorId(id);
            fornecedor.Telefone = model.Telefone;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw new Exception("Não foi possivel atualizar o dado, tente mais tarde!");
            }
        }

        public async Task DeletarFornecedor(int? id)
        {
            var fornecedor = await BuscarPorId(id);
            if(fornecedor == null) Results.NotFound();
            try
            {
                _db.Remove(fornecedor);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw new Exception("Não foi possivel deletar o dado, tente mais tarde!");
            }
        }
    }
}