using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.fornecedores.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        Task<T> GetById(int? id);
        void Create(T model);
        void Delete(int? id);
        void Update(T model, int? id);
    }
}