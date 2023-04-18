using System.ComponentModel.DataAnnotations;

namespace icarus.jwtManager.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(35)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(85)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Senha { get; set; }

        public string Descricao { get; set; }
    }
}
