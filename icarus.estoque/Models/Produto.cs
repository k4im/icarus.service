using System.ComponentModel.DataAnnotations;

namespace icarus.estoque.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo obrigatório")]
        [DataType("NVARCHAR(85)")]
        public string Nome { get; set; }


        [Required(ErrorMessage ="Campo obrigatório")]
        [DataType("NVARCHAR(85)")]
        public string Cor { get; set; }


        [Required(ErrorMessage ="Campo obrigatório")]
        public int Quantidade { get; set; }


        [Required(ErrorMessage ="Campo obrigatório")]
        public int ValorUnitario { get; set; }
    }
}