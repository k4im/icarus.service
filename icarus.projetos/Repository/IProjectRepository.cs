using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models;

namespace icarus.projetos.Repository
{
    public interface IProjectRepository
    {
        string LastSearchTxt { get; set; }

        Task<ProjectResponseDTO> GetAllProjects(int pagina);
        
        Task CreateProject(Project model);
        
        Task UpdateProject(ProjectUpdate model, int? id);
        
        Task DeleteProject(int? id);
        Task<Project> GetById(int? id);

    }
}