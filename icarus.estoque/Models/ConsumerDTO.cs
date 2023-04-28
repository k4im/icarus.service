using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.estoque.Models
{
    public class ConsumerDTO
    {
        public List<ProjectDTO> Projetos { get; set; } = new List<ProjectDTO>();
    }
}