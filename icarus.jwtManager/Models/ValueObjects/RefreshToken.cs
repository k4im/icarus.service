using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models.ValueObjects.interfaces;

namespace icarus.jwtManager.Models.ValueObjects
{
    public class RefreshToken : IToken
    { 
        public RefreshToken(string token)
        {
            Token = token;
            DataCriacao = DateTime.UtcNow;
            DataExpiracao = DateTime.UtcNow.AddHours(1);
        }

        public string Token { get;}
        public DateTime DataCriacao { get;}
        public DateTime DataExpiracao { get;}


        public bool validarExpiracao()
        {
            if(this.DataExpiracao < DateTime.UtcNow) return true;
            return false;
        }
    }
}