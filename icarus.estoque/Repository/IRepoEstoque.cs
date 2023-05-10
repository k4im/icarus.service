using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.estoque.Models;

namespace icarus.estoque.Repository
{
    public interface IRepoEstoque
    {
        Task<EstoqueResponseDTO> BuscarProdutos(int pagina, float resultadoPorPagina);
        Task<Produto> BuscarProdutoId(int? id);
        Task NovoProduto(Produto modelo);
        Task DeletarProduto(int? id);
        Task AtualizarProduto(int? id, Produto modelo);
        Task TratarMessage(ConsumerDTO consumer);
        Task ValidarProdutos();
    }
}