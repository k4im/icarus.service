using System.ComponentModel.DataAnnotations;

namespace icarus.estoque.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        [DataType("NVARCAHR(60)")]
        public string? Name { get; set; }
    
        [Required(ErrorMessage ="Campo é obrigatório")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Campo é obrigatório")]
        [DataType("NVARCAHR(20)")]
        public string? Cor { get; set; }
    
        [Required(ErrorMessage = "Campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        
    }
}