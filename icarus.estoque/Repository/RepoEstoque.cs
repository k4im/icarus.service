using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.estoque.Data;
using icarus.estoque.Models;

namespace icarus.estoque.Repository
{
    public class RepoEstoque : IRepoEstoque
    {
        private readonly DataContext _db;

        public RepoEstoque(DataContext db)
        {
            _db = db;
        }
        public List<Produto> BuscarProdutos()
        {
            var produtos =  _db.Produtos.ToList();
            if(produtos == null) Results.NotFound();
            return produtos;
        }

        public async Task<Produto> BuscarPorId(int? id)
        {
            var produto = await _db.Produtos.FindAsync(id);
            if(produto == null) Results.NotFound();
            return produto;
        }



        public async void Create(Produto model)
        {
            if(model != null) {
                await _db.Produtos.AddAsync(model);
                await _db.SaveChangesAsync();
            }
        }

        public async void Delete(int? id)
        {
            var produto = await _db.Produtos.FindAsync(id);
            if(produto == null) Results.NotFound();
            _db.Produtos.Remove(produto);
            await _db.SaveChangesAsync();

        }
        
        public async void Update(Produto model, int? id)
        {
                var item = _db.Produtos.FirstOrDefault(x => x.Id == id);
                if(item == null) Results.NotFound();
                item.Nome = model.Nome;
                _db.Produtos.Update(item);
                await _db.SaveChangesAsync();
        }
    }
}