using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models
{
    public class UsuarioDTO
    {

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(35)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Senha { get; set; }

    }
}