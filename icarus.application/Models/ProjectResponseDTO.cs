using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.application.models;

namespace icarus.application.Models
{
    public class ProjectResponseDTO
    {
        public List<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        
        public int PageCount {get; set;}
    }
}