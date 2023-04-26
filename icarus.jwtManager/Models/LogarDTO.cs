using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models
{
    public class LogarDTO
    {
        public bool SucessoAoLogar { get; set; } = false;
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}