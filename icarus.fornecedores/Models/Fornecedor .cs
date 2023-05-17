using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Models.ValueObjects;

namespace icarus.fornecedores.Models
{
    public class Fornecedor 
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(45)")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(30)")]
        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public string Telefone { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}