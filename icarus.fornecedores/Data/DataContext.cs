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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API for specifying concurrency token
            modelBuilder.Entity<Fornecedor>()
                .Property(fornecedor => fornecedor.RowVersion)
                .IsConcurrencyToken();
        }
        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}