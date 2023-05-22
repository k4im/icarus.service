using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.estoque.Models.ValueObjects
{
    public class Valor
    {
        public double ValorDoItem { get; private set; }
        
        protected Valor(){}
        public Valor(double valorDoItem)
        {
            validarValorNegativo(valorDoItem);
            ValorDoItem = setarValor(valorDoItem);
        }


        void validarValorNegativo(double valor)
        {
            if(valor < 0) throw new Exception("O valor do item não pode ser negativo!");
        }

        double setarValor(double valor)
        {
            var valorCorrigido = Math.Round(valor, 2);
            return valorCorrigido;
        }

    }
}