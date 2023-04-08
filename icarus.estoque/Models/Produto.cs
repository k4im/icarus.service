using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.estoque.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo não pode ser nulo")]
        [DataType("NVARCHAR(30)")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Este campo não pode ser nulo")]
        public int Quantidade { get; set; }
    
        [Required(ErrorMessage = "Este campo não poder ser nulo")]
        [DataType("NVARCHAR(25)")]
        public string Cor { get; set; }
   
        public double Altura { get; set; }
           
        public double Largura { get; set; }

        [Required(ErrorMessage = "Este campo não pode ser nulo")]
        public double  ValorUnitario { get; set; }

    }


}