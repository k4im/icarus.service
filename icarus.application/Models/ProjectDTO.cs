using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.application.models
{
    public class ProjectDTO
    {

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Status { get; set; }        

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataInicio { get; set; }  

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataEntrega { get; set; } 

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Descricao { get; set; } 

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int Valor { get; set; }

        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public bool IsValid { get; internal set; }
    }
}