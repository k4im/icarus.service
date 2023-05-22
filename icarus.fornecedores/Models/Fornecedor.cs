using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using icarus.fornecedores.Models.ValueObjects;

namespace icarus.fornecedores.Models
{
    public class Fornecedor 
    {

        protected Fornecedor()
        {}
        public Fornecedor(string nome, CadastroNacionalPessoaJuridica cnpj, Endereco endereco, Telefone telefone)
        {
            ValidarNome(nome);
            Nome = nome;
            Cnpj = cnpj;
            Endereco = endereco;
            Telefone = telefone;
        }

        [Key]
        public int Id { get;  set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(45)")]
        public string Nome { get;  private set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public CadastroNacionalPessoaJuridica Cnpj { get;  private set; }

        [DataType("NVARCHAR(30)")]
        public Endereco Endereco { get;  private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public Telefone Telefone { get;  private set; }

        [Timestamp]
        public byte[] RowVersion { get;  private set; }
    

        public void MudarTelefone(Telefone novoTelefone)
        {
            this.Telefone = novoTelefone;
        }

        public void MudancaDeEndereco(Endereco novoEndereco)
        {
            this.Endereco = novoEndereco;
        }

        void ValidarNome(string nome)
        {
            if (!Regex.IsMatch(nome, @"^[a-zA-Z ]+$")) throw new Exception("Nome não pode conter caracteres especiais");
        }
    }
}