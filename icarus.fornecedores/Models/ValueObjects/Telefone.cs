using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.fornecedores.Models.ValueObjects
{
    public class Telefone
    {

        [DataType("VARCHAR(2)")]
        public string CodigoPais { get; set; }
        
        [DataType("VARCHAR(2)")]
        public string CodigoDeArea { get; set; }
        public string Numero { get; set; }


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
            if (!System.Text.RegularExpressions.Regex.IsMatch(codigoPais, "^[0-9]*$")) throw new Exception("Campo precisa conter apenas numeros");
            if (!System.Text.RegularExpressions.Regex.IsMatch(codigoDeArea, "^[0-9]*$")) throw new Exception("Campo precisa conter apenas numeros");
            if (!System.Text.RegularExpressions.Regex.IsMatch(numero, "^[0-9]*$")) throw new Exception("Campo precisa conter apenas numeros");
        }
    }
}