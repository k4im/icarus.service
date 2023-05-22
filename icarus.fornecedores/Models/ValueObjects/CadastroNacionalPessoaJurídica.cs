using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icarus.fornecedores.Models.ValueObjects
{
    public class CadastroNacionalPessoaJuridica
    {
        protected CadastroNacionalPessoaJuridica()
        {}
        
        public CadastroNacionalPessoaJuridica(string cnpj)
        {
            ValidarCnpj(cnpj);
            Cnpj = cnpj;
        }

        public string Cnpj { get; private set; }

        void ValidarCnpj(string cnpj)
        {
            if (!Regex.IsMatch(cnpj, "^[0-9]*$")) throw new Exception("Insira apenas os numeros do cnpj");
            if(string.IsNullOrEmpty(cnpj)) throw new Exception("O cnpj não pode estar nulo!");
            if(cnpj.Length < 14) throw new Exception("O cnpj deve conter no minimo 14 caracteres!");
        }

    }
}