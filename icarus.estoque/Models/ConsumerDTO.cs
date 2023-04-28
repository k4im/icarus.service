using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.estoque.Models
{
    public class ConsumerDTO
    {
        public List<ProjectDTO> projetos { get; set; } = new List<ProjectDTO>();
    }
}