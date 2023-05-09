using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models
{
    public class LoginDTO
    {
        public string ChaveDeAcesso { get; set; }
        public string Senha { get; set; }
    }
}