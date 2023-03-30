using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models;

namespace icarus.projetos.Repository
{
    public interface IProjectRepository
    {
        public string LastSearchTxt { get; set; }

        public Task<ProjectResponseDTO> GetAllProjects();
        public Task<ProjectResponseDTO> GetProjects(int page = 1);

        public Task<ProjectResponseDTO>  GetProjectsFilter(string SearchFilter);
        public Task<ProjectResponseDTO> GetProjectsFilterPagination(string SearchFilter,  int page = 1);
        
        public Task<ProjectDTO> CreateProject(Project model);
        
        public Task UpdateProject(Project model, int id);
        
        public Task DeleteProject(int id);

    }
}