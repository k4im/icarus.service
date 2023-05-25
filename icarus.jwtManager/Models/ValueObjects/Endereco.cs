using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models.ValueObjects
{
    public class Endereco
    {
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Rua { get; private set; }
        public string Cep { get; private set; }
        public int Numero { get; private set; }


        protected Endereco()
        {}
        
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
            if(!Regex.IsMatch(cidade, @"^[a-zA-Z ]+$")) throw new Exception("A cidade não pode conter caracteres especiais");

        }
        
        void VerificarBairro(string bairro)
        {
            if(string.IsNullOrEmpty(bairro)) throw new Exception("O Bairro não pode estar vazio!");
            if(!Regex.IsMatch(bairro, @"^[a-zA-Z ]+$")) throw new Exception("O bairro não pode conter caracteres especiais");

        }
        void VerificarCep(string cep)
        {
            if(!Regex.IsMatch(cep, @"^[a-zA-Z ]+$")) throw new Exception("O cep não pode conter caracteres especiais");
            if(string.IsNullOrEmpty(cep)) throw new Exception("O Cep não pode estar vazio!");
            
        }
        void VerificarRua(string rua)
        {
            if (!Regex.IsMatch(rua, @"^[a-zA-Z ]+$")) throw new Exception("A rua não pode conter caracteres especiais");
            if(string.IsNullOrEmpty(rua)) throw new Exception("A Rua não pode estar vazio!");
        }
    }
}