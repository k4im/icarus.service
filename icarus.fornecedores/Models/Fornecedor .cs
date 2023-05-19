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
        public int Id { get; private set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(45)")]
        public string Nome { get; private set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public string Cnpj { get; private set; }

        [DataType("NVARCHAR(30)")]
        public Endereco Endereco { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType("NVARCHAR(14)")]
        public Telefone Telefone { get; private set; }

        [Timestamp]
        public byte[] RowVersion { get; private set; }
    
        public Fornecedor(string nome, string cnpj, Endereco endereco, Telefone telefone)
        {
            ValidarRegex(nome, cnpj);
            Nome = nome;
            Cnpj = cnpj;
            Endereco = endereco;
            Telefone = telefone;
        }

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
            if (!System.Text.RegularExpressions.Regex.IsMatch(nome, @"^[ A-Za-z0-9]$")) throw new Exception("Nome não pode conter caracteres especiais");
            if (!System.Text.RegularExpressions.Regex.IsMatch(cnpj, @"^[ A-Za-z0-9]$")) throw new Exception("CNPJ não pode conter caracteres especiais");

        }
    }
}