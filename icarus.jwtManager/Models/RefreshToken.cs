using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.jwtManager.Models
{
    public class RefreshToken
    {
        public string UserEmail { get; set; }
        public string TokenRefresh { get; set; }
    }
}