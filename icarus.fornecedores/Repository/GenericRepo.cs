using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Data;

namespace icarus.fornecedores.Repository
{
    public class GenericRepo<T> : IGenericRepository<T> where T : class
    {

        private readonly DataContext _db;

        public GenericRepo(DataContext db)
        {
            _db = db;
        }
        public List<T> GetAll()
        {
            var itens = _db.Set<T>().ToList();
            return itens;   
        }

        public async Task<T> GetById(int? id)
        {
            var item = await _db.Set<T>().FindAsync(id);
            if(item != null) Results.NotFound();
            return item;
        }

        public async void Create(T model)
        {
            await _db.Set<T>().AddAsync(model);
            await _db.SaveChangesAsync();

        }

        public async void Delete(int? id)
        {
            var item = await GetById(id);
            if(item == null) Results.NotFound();
            _db.Set<T>().Remove(item);
            await _db.SaveChangesAsync();
        }

        public async void Update(T model, int? id)
        {
            if(model != null && id != null) {
                var item = await GetById(id);
                if (item == null) Results.NotFound();
                item = model;
                _db.Set<T>().Update(item);
                await _db.SaveChangesAsync();
            }
        }
    }
}