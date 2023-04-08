using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.estoque.Models;

namespace icarus.estoque.Repository
{
    public interface IRepoEstoque
    {
        List<Produto> BuscarProdutos();

        Task<Produto> BuscarPorId(int? id);

        void Create(Produto model);

        void Delete(int? id);

        void Update(Produto model, int? id);

    }
}