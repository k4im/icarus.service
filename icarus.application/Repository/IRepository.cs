using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.application.models;

namespace icarus.application.Repository
{
    public interface IRepository
    {
        Task Get(string route);
        Task<String> GetFilter(string route, string search);
        public string LastSearch { get; set; }
        Task Delete(string route, int id);
        Task Create(string route, ProjectDTO model);
        Task Update(string route, int id, ProjectDTO model);
    }
}