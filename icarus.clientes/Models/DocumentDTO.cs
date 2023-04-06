using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.clientes.Models
{
    public class DocumentDTO
    {
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(50)")]
        public required string DocumentName { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [DataType("NVARCHAR(5)")]
        public required string DocumentExtension { get; set; }       
    }
}