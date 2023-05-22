using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.projetos.models.ValueObject
{
    public class DescricaoProjeto
    {
        public string Descricao { get; private set; }   

        protected DescricaoProjeto()
        {}

        public DescricaoProjeto(string descricao)
        {
            Descricao = descricao;
        }

    }
}