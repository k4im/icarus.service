using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.estoque.Models.ValueObjects
{
    public class Quantidade
    {
        public int QuantidadeItem { get; private set; }

        protected Quantidade()
        {}
        public Quantidade(int quantidadeItem) 
        {
            NaoPodeSerMenorQueZero(quantidadeItem);
            QuantidadeItem = quantidadeItem;
        }
        void NaoPodeSerMenorQueZero(int quantidadeItem)
        {
            if(quantidadeItem < 0) throw new Exception("A quantidade de item não pode ser um valor negativo!");
        }

    
        public void EntradaDeProdutos(int quantidade)
        {
            this.QuantidadeItem += quantidade;
        }

        public void SaidaProdutos(int quantidade)
        {
            this.QuantidadeItem  -= quantidade;
        }
    }
}