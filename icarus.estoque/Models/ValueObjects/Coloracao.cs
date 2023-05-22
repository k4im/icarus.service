using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icarus.estoque.Models.ValueObjects
{
    public class Coloracao
    {

        public string Cor { get; private set; }
        
        protected Coloracao(){}
        public Coloracao(string cor)
        {
            Cor = cor;
        }

        void validarColoracao(string cor)
        {
            if(string.IsNullOrEmpty(cor)) throw new Exception("O valor da cor não pode ser nulo!");
            if(!Regex.IsMatch(cor, @"^[a-zA-Z ]+$")) throw new Exception("O valor da cor não pode conter caracteres especiais");

        }
    }
}