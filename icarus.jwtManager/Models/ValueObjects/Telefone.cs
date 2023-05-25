using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models.ValueObjects
{
    public class Telefone
    {
        [DataType("VARCHAR(2)")]
        public string CodigoPais { get; private set; }
        
        [DataType("VARCHAR(2)")]
        public string CodigoDeArea { get; private set; }
        public string Numero { get; private set; }

        protected Telefone()
        {}

        public Telefone(string codigoPais, string codigoDeArea, string numero)
        {
            validarRegex(codigoPais, codigoDeArea, numero);
            validarCodigoDeArea(codigoDeArea);
            CodigoPais = codigoPais;
            CodigoDeArea = codigoDeArea;
            Numero = numero;
        }

        void validarCodigoDeArea(string codigoDeArea)
        {
            if(string.IsNullOrEmpty(codigoDeArea)) throw new Exception("O DDD precisa ser preenchido");
            if(codigoDeArea.Length > 2) throw new Exception("O DDD precisa conter dois numeros!");
            if(codigoDeArea.Length < 2) throw new Exception("O DDD precisa conter dois numeros!");
        }

        void validarRegex(string codigoPais, string codigoDeArea, string numero)
        {
            if (!Regex.IsMatch(codigoPais, "^[0-9]*$")) throw new Exception("Codigo do país precisa conter apenas numeros");
            if (!Regex.IsMatch(codigoDeArea, "^[0-9]*$")) throw new Exception("Codigo de area precisa conter apenas numeros");
            if (!Regex.IsMatch(numero, "^[0-9]*$")) throw new Exception("Numero de telefone precisa conter apenas numeros");
        }
    }
}