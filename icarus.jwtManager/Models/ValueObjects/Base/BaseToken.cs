using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models.ValueObjects.Base
{
    public class BaseToken
    {
        public BaseToken(string token)
        {
            Token = token;
            DataDeCriacao = DateTime.UtcNow;
            DataDeExpiracao = DateTime.UtcNow.AddHours(1);
        }

        public string Token {get;}
        public DateTime DataDeCriacao {get;}
        public DateTime DataDeExpiracao {get;}
    }
}