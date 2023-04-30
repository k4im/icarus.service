using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.fornecedores.Models
{
    public class FornecedorDTO
    {
                
        public string Nome { get; set; }
        
        public string Cnpj { get; set; }

        public string Endereco { get; set; }

        public string Cep { get; set; }

        public string Telefone { get; set; }
    }
}