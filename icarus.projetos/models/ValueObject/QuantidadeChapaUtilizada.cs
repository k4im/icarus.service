using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models.ValueObject
{
    public class QuantidadeChapaUtilizada
    {
        public int Quantidade { get; private set; }

        protected QuantidadeChapaUtilizada()
        {}
        public QuantidadeChapaUtilizada(int quantidade)
        {
            ValidarQuantidade(quantidade);
            Quantidade = quantidade;
        }

        void ValidarQuantidade(int quantidade)
        {
            if(quantidade < 0) throw new Exception("A quantidade não pode ser negativa!");
        }

    }
}