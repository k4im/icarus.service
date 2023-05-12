using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models;
using Microsoft.EntityFrameworkCore;

namespace icarus.projetos.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API for specifying concurrency token
            modelBuilder.Entity<Project>()
                .Property(projeto => projeto.RowVersion)
                .IsConcurrencyToken();
        }
        public DbSet<Project> Projetos { get; set; } 
        public DbSet<ProjectFile> Arquivos { get; set; }
    }
}