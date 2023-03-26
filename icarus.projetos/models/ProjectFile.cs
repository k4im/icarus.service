using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models
{
    public class ProjectFile
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(150)")]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(150)")]
        public string Extension { get; set; }
    }
}