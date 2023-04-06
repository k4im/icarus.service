using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.fornecedores.Models
{
    public class SupllyDTO
    {
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(80)")]
        public required string Name { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(16)")]
        public required string Phone { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(18)")]
        public required string Cnpj { get; set; }
        
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(16)")]
        public required string City { get; set; }
        
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(50)")]
        public required string Neighborhood { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(50)")]
        public required string Street { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(20)")]
        public required string HouseNumber { get; set; }
        
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(20)")]
        public required string Cep { get; set; }


    }
}