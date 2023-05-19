using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Models;
using icarus.fornecedores.Models.ValueObjects;

namespace icarus.fornecedores.Repository
{
    public interface IRepoFornecedor
    {
        Task<ResponseDTO> BuscarFornecedores(int pagina, float resulatdoPorPagina);

        Task<Fornecedor> BuscarPorId(int? id);

        Task CriarFornecedor(Fornecedor model);

        Task DeletarFornecedor(int? id);

        Task TrocaDeTelefone(int? id, Telefone model);
        Task TrocaDeEndereco(int? id, Endereco model);
    }
}