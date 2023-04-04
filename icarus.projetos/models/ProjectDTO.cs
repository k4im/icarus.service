using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models
{
    public class ProjectDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Status { get; set; }        

        public DateTime DataInicio { get; set; }  

        public DateTime DataEntrega { get; set; } 

        public string Descricao { get; set; } 

        public decimal Valor { get; set; }
    }
}