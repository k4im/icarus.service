using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models.ValueObject
{
    public class ValorProjeto   
    {
       
        public double Valor { get; private set; }
        
        protected ValorProjeto(){}
        public ValorProjeto(double valor)
        {
            ValidarValor(valor);
            Valor = valor;
        }

        void ValidarValor(double valor)
        {
            if(valor < 0) throw new Exception("O valor não pode estar negativo!");
        }

    }
}