using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Models;

namespace icarus.fornecedores.Repository
{
    public interface IRepoFornecedor
    {
        Task<ResponseDTO> BuscarFornecedores(int pagina, float resulatdoPorPagina);

        Task<Fornecedor> BuscarPorId(int? id);

        Task CriarFornecedor(Fornecedor model);

        Task DeletarFornecedor(int? id);

        Task AtualizarFornecedor(int? id, Fornecedor model);
    }
}