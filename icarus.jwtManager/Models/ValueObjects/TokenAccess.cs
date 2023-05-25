using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models.ValueObjects.Base;

namespace icarus.jwtManager.Models.ValueObjects
{
    public class TokenAccess : BaseToken
    {
        public TokenAccess(string token) : base(token)
        {}
    }
}