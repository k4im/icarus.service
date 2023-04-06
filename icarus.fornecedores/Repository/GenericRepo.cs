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

        public void Create(T model)
        {

        }

        public void Delete(int? id)
        {
        }

        public void Update(T model, int? id)
        {
            throw new NotImplementedException();
        }
    }
}