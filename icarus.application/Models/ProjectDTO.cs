using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.application.models
{
    public class ProjectDTO
    {

        public string Name { get; set; }
        
        public string Status { get; set; }        

        public DateTime DataIncio { get; set; }  

        public DateTime DataEntrega { get; set; } 

        public string Descricao { get; set; } 

        public int Valor { get; set; }

        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}