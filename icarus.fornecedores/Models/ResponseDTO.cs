using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.fornecedores.Models
{
    public class ResponseDTO
    {
        public List<Fornecedor> Fornecedores { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
    }
}