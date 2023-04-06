using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models
{
    public class ProjectUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(100)")]
        public string Status { get; set; }        
        public DateTime DataIncio { get; set; }  

        public DateTime DataEntrega { get; set; } 

        public string Descricao { get; set; } 
        public decimal Valor { get; set; }

    }
}