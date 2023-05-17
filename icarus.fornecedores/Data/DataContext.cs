using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Models;
using icarus.fornecedores.Models.ValueObjects;
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
            
            modelBuilder.Entity<Fornecedor>(builder => {
            
                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Cidade)
                    .HasColumnName("Cidade")
                    .IsRequired(true);

                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Bairro)
                    .HasColumnName("Bairro")
                    .IsRequired(true);


                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Rua)
                    .HasColumnName("Rua")
                    .IsRequired(true);
                    
                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Cep)
                    .HasColumnName("Cep")
                    .IsRequired(true);

                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Numero)
                    .HasColumnName("Numero")
                    .IsRequired(true);
            
            });
                
               
        }
        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}