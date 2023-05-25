using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models.ValueObjects.Base;

namespace icarus.jwtManager.Models.ValueObjects
{
    public class RefreshToken : BaseToken
    {
        public RefreshToken(string token) : base(token)
        { }
    }
}