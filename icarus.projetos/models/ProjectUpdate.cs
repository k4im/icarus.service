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
        // [Key]
        public int Id { get; set; }

        // [Required(ErrorMessage = "Campo obrigatório")]
        // [DataType("NVARCHAR(150)")]
        public string Name { get; set; }
        
        // [Required(ErrorMessage = "Campo obrigatório")]
        // [DataType("NVARCHAR(100)")]
        public string Status { get; set; }        

        // [Required(ErrorMessage = "Campo obrigatório")]
        // [DataType("DATE")]
        public DateTime DataIncio { get; set; }  

        // [Required(ErrorMessage = "Campo obrigatório")]
        // [DataType("DATE")]
        public DateTime DataEntrega { get; set; } 

        // [Required(ErrorMessage = "Campo obrigatório")]
        // [DataType("NVARCHAR(350)")]
        public string Descricao { get; set; } 

        // [Required(ErrorMessage = "Campo obrigatório")]
        public decimal Valor { get; set; }

    }
}