using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.clientes.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(150)")]
        public required string Name { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(350)")]
        public required string Phone { get; set; }
        
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(150)")]
        public required string City { get; set; }
        
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(150)")]
        public required string Neighborhood { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(150)")]
        public required string Street { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(20)")]
        public required string HouseNumber { get; set; }
        
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(20)")]
        public required string Cep { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        public bool Documents { get; set; }
        
    }
}