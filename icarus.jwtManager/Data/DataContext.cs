using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models;
using Microsoft.EntityFrameworkCore;

namespace icarus.jwtManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Usuario> Usuarios { get; set; }
    }
}