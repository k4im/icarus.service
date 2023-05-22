using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.estoque.Models;
using icarus.estoque.Models.ValueObjects;

namespace icarus.estoque.Repository
{
    public interface IRepoEstoque
    {
        Task<EstoqueResponseDTO> BuscarProdutos(int pagina, float resultadoPorPagina);
        Task<Produto> BuscarProdutoId(int? id);
        Task NovoProduto(Produto modelo);
        Task DeletarProduto(int? id);
        Task EntradaDeProdutos(int? id, Quantidade modelo);
        Task TratarMessage(ConsumerDTO consumer);
        Task ValidarProdutos();
    }
}