using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models.ValueObjects
{
    public class Nome
    {
        protected Nome(){}
        public Nome(string primeiroNome, string sobreNome)
        {
            PrimeiroNome = validarPrimeiroNome(primeiroNome);
            SobreNome = sobreNome;
        }

        public string PrimeiroNome { get;}
        public string SobreNome { get;}

        string validarPrimeiroNome(string PNome)
        {
            if(string.IsNullOrEmpty(PNome)) throw new Exception("O nome não pode estar vazia!");
            if(!Regex.IsMatch(PNome, @"^[a-zA-Z ]+$")) throw new Exception("O nome não pode conter caracteres especiais");
            return PNome.ToUpper().Trim();
        }
        string validarSobreNome(string SobreNome)
        {
            if(string.IsNullOrEmpty(SobreNome)) throw new Exception("O sobrenome não pode estar vazia!");
            if(!Regex.IsMatch(SobreNome, @"^[a-zA-Z ]+$")) throw new Exception("O sobrenome não pode conter caracteres especiais");
            return SobreNome.ToUpper().Trim();
        }
        
    }
}