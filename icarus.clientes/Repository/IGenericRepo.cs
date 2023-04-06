using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.clientes.Repository
{
    public interface IGenericRepo<T> where T : class
    {   
        List<T> GetAll();
        Task<T> GetById(int? id);
        void Create(T model);
        void Update(T model, int? id);
        void Delete(int? id);
    }
}