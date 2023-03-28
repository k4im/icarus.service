using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models
{
    public class ProjectResponseDTO
    {
        
        public List<Project> Projects { get; set; } = new List<Project>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string LastSearch { get; set; }
    }
}