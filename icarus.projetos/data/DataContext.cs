using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models;
using icarus.projetos.models.ValueObject;
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
        
        
            modelBuilder.Entity<Project>(builder => {
                builder.OwnsOne<StatusProjeto>(projeto => projeto.Status)
                .Property(status => status.Status)
                .HasColumnName("Status");
            });

            modelBuilder.Entity<Project>(builder => {
                builder.OwnsOne<ChapaUtilizada>(projeto => projeto.Chapa)
                .Property(chapa => chapa.Chapa)
                .HasColumnName("ChapaUtilizada");
            });

            modelBuilder.Entity<Project>(builder => {
                builder.OwnsOne<DescricaoProjeto>(projeto => projeto.Descricao)
                .Property(descricao => descricao.Descricao)
                .HasColumnName("Descricao");
            });

            modelBuilder.Entity<Project>(builder => {
                builder.OwnsOne<QuantidadeChapaUtilizada>(projeto => projeto.QuantidadeDeChapa)
                .Property(quantidadeChapa => quantidadeChapa.Quantidade)
                .HasColumnName("QtndChapa");
            });

            modelBuilder.Entity<Project>(builder => {
                builder.OwnsOne<ValorProjeto>(projeto => projeto.Valor)
                .Property(valor => valor.Valor)
                .HasColumnName("ValorDoProjeto");
            });
        }
        public DbSet<Project> Projetos { get; set; } 
    }
}