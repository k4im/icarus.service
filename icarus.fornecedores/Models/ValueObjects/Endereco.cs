using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace icarus.fornecedores.Models.ValueObjects
{
    public class Endereco
    {
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Rua { get; private set; }
        public string Cep { get; private set; }
        public int Numero { get; private set; }

        public Endereco(string cidade, string bairro, string rua, string cep, int numero)
        {

            VerificarCampos(cidade, bairro, rua, cep, numero);
            Cidade = cidade;
            Bairro = bairro;
            Rua = rua;
            Cep = cep;
            Numero = numero;
        }

       
        void VerificarCampos(string cidade, string bairro, string cep, string rua, int numero)
        {
            VerificarCidade(cidade);
            VerificarBairro(bairro);
            VerificarRua(rua);
            VerificarCep(cep);
        }

        void VerificarCidade(string cidade)
        {
            if(string.IsNullOrEmpty(cidade)) throw new Exception("A cidade não pode estar vazia!");
            if (System.Text.RegularExpressions.Regex.IsMatch(cidade, @"^[ A-Za-z0-9]$")) throw new Exception("A cidade não pode conter caracteres especiais");

        }
        
        void VerificarBairro(string bairro)
        {
            if(string.IsNullOrEmpty(bairro)) throw new Exception("O Bairro não pode estar vazio!");
            if (System.Text.RegularExpressions.Regex.IsMatch(bairro, @"^[ A-Za-z0-9]$")) throw new Exception("O bairro não pode conter caracteres especiais");

        }
        void VerificarCep(string cep)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(cep, @"^[ A-Za-z0-9]$")) throw new Exception("O cep não pode conter caracteres especiais");
            if(string.IsNullOrEmpty(cep)) throw new Exception("O Cep não pode estar vazio!");
            // if(cep.Length < 8) throw new Exception("O cep precisa conter entre 8 e 9 caracteres");
            // if(cep.Length > 9) throw new Exception("O cep precisa conter entre 8 e 9 caracteres");

        }
        void VerificarRua(string rua)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(rua, @"^[ A-Za-z0-9]$")) throw new Exception("A rua não pode conter caracteres especiais");
            if(string.IsNullOrEmpty(rua)) throw new Exception("A Rua não pode estar vazio!");
        }
    }

    
}