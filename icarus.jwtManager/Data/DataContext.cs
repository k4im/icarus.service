using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace icarus.jwtManager.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {}

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}