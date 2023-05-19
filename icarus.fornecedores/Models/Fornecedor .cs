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
        public Fornecedor(string nome, string cnpj, Endereco endereco, Telefone telefone)
        {
            ValidarRegex(nome, cnpj);
            Nome = nome;
            Cnpj = cnpj;
            Endereco = endereco;
            Telefone = telefone;
        }

        [Key]
        public int Id { get;  set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(45)")]
        public string Nome { get;  set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public string Cnpj { get;  set; }

        [DataType("NVARCHAR(30)")]
        public Endereco Endereco { get;  set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public Telefone Telefone { get;  set; }

        [Timestamp]
        public byte[] RowVersion { get;  set; }
    

        public void MudarTelefone(Telefone novoTelefone)
        {
            this.Telefone = novoTelefone;
        }

        public void MudancaDeEndereco(Endereco novoEndereco)
        {
            this.Endereco = novoEndereco;
        }

        void ValidarRegex(string nome, string cnpj)
        {
            if (!Regex.IsMatch(nome, @"^[a-zA-Z]+$")) throw new Exception("Nome não pode conter caracteres especiais");
            if (!Regex.IsMatch(cnpj, @"^[a-zA-Z]+$")) throw new Exception("CNPJ não pode conter caracteres especiais");

        }
    }
}