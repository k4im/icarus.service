using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models
{
    public class ProjectResponseDTO
    {
        
        public List<Project> Projects { get; set; } = new List<Project>();
        public int Paginas { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
    }
}