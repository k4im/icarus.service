using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.estoque.Models
{
    public class EstoqueResponseDTO
    {
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public int Paginas { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
    }
}