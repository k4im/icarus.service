using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models
{
    public class RefreshTokenDTO
    {

        public string ChaveDeAcesso { get; set; }
        public string Token { get; set; }        
        public string RefreshToken { get; set; } 
        public DateTime CriadoEm { get; set; }
        public DateTime ExpiraEm { get; set; }       
    }
}