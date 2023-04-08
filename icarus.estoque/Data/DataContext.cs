using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace icarus.estoque.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Produto> Produtos { get; set; }
    }
}