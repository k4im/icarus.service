using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Models;
using Microsoft.EntityFrameworkCore;

namespace icarus.fornecedores.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Suplly> Fornecedores { get; set; }
    }
}