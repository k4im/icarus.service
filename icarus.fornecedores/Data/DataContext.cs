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
                    .IsRequired(false);

                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Bairro)
                    .HasColumnName("Bairro")
                    .IsRequired(false);


                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Rua)
                    .HasColumnName("Rua")
                    .IsRequired(false);
                    
                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Cep)
                    .HasColumnName("Cep")
                    .IsRequired(false);

                builder.OwnsOne<Endereco>(fornecedor => fornecedor.Endereco)
                    .Property(endereco => endereco.Numero)
                    .HasColumnName("NumeroEndereco")
                    .IsRequired(true);
            
            });

            modelBuilder.Entity<Fornecedor>(builder =>{
                
                builder.OwnsOne<Telefone>(fornecedor => fornecedor.Telefone)
                    .Property(telefone => telefone.CodigoPais)
                    .HasColumnName("CodigoDoPais")
                    .IsRequired(false);

                builder.OwnsOne<Telefone>(fornecedor => fornecedor.Telefone)
                    .Property(telefone => telefone.CodigoDeArea)
                    .HasColumnName("CodigoDeArea")
                    .IsRequired(false);
                
                builder.OwnsOne<Telefone>(fornecedor => fornecedor.Telefone)
                    .Property(telefone => telefone.Numero)
                    .HasColumnName("NumeroTelefone")
                    .IsRequired(false);
            });

            modelBuilder.Entity<Fornecedor>(builder => {
               builder.OwnsOne<CadastroNacionalPessoaJuridica>(fornecedor => fornecedor.Cnpj)
                .Property(cnpj => cnpj.Cnpj)
                .HasColumnName("CNPJ"); 
            }); 
               
        }
        public DbSet<Fornecedor> Fornecedores { get; set; }
    }
}