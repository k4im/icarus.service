using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models;
using icarus.projetos.models.ValueObject;

namespace icarus.projetos.Repository
{
    public interface IProjectRepository
    {
        string LastSearchTxt { get; set; }

        Task<ProjectResponseDTO> BuscarProdutos(int pagina, float resultadoPorPagina);
        
        Task CriarProjeto(Project model);
        
        Task AtualizarStatus(StatusProjeto model, int? id);
        
        Task DeletarProjeto(int? id);
        Task<Project> BuscarPorId(int? id);

    }
}