using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace icarus.jwtManager.Models
{
    public class AppUser : IdentityUser
    {
        public string Name {get;}
        public string Role {get; set;}
    }
}