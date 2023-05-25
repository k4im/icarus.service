using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models.ValueObjects
{
    public class Perfilamento
    {
        protected Perfilamento(){}
        public Perfilamento(string perfil)
        {
            Perfil = validarPerfil(perfil);
        }

        public string Perfil {get;}

        string validarPerfil(string perfil)
        {
            if(string.IsNullOrEmpty(perfil)) throw new Exception("O perfil não pode estar vazia!");
            if(!Regex.IsMatch(perfil, @"^[a-zA-Z ]+$")) throw new Exception("O perfil não pode conter caracteres especiais");
            return perfil.ToUpper().Trim();
        }
    }
}