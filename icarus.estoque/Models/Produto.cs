using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using icarus.estoque.Models.ValueObjects;

namespace icarus.estoque.Models
{
    public class Produto
    {

        [Key]
        public int Id { get; set;}

        [Required(ErrorMessage ="Campo obrigatório")]
        [DataType("NVARCHAR(85)")]
        public string Nome { get; private set; }


        [DataType("NVARCHAR(85)")]
        public Coloracao Cor { get; private set; }


        [Required(ErrorMessage ="Campo obrigatório")]
        [ConcurrencyCheck]
        public Quantidade QuantidadeProduto { get; private set; }


        [Required(ErrorMessage ="Campo obrigatório")]
        public Valor ValorUnitario { get; private set; }

        [Timestamp]
        public byte[] RowVersion { get; private set; }
      
        protected Produto() {}

        public Produto(string nome, Coloracao cor, Quantidade quantidadeProduto, Valor valorUnitario)
        {
            validarNome(nome);
            Nome = nome;
            Cor = cor;
            QuantidadeProduto = quantidadeProduto;
            ValorUnitario = valorUnitario;
        }

        public void EntradaDeProduto(int quantidade)
        {
            this.QuantidadeProduto.EntradaDeProdutos(quantidade);
        }
    
        public void SaidaDeProduto(int quantidade)
        {
            this.QuantidadeProduto.SaidaProdutos(quantidade);
        }
    
        void validarNome(string nome)
        {
            if(!Regex.IsMatch(nome, @"^[a-zA-Z ]+$")) throw new Exception("O nome não pode conter caracteres especiais");
            if(string.IsNullOrEmpty(nome)) throw new Exception("O nome não pode ser nulo!");
        }
    }
}