using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(25)")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(25)")]
        public string Status { get; set; }        

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("DATE")]
        public DateTime DataInicio { get; set; }  

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("DATE")]
        public DateTime DataEntrega { get; set; } 

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(150)")]
        public string Chapa { get; set; } 

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(150)")]
        public string Descricao { get; set; } 

        [Required(ErrorMessage = "Campo obrigatório")]
        public int QuantidadeDeChapa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal Valor { get; set; }

    }
}