using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models
{
    public class RefreshTokenDTO
    {

        public string UserName { get; set; }
        public string Token { get; set; }        
        public string RefreshToken { get; set; }        
    }
}