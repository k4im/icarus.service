using icarus.estoque.Models;
using icarus.estoque.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace icarus.estoque.Data
{
    public class DataContextEstoque : DbContext
    {
        public DataContextEstoque(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API for specifying concurrency token
                
            modelBuilder.Entity<Produto>()
                .Property(produto => produto.RowVersion)
                .IsConcurrencyToken();
        
            modelBuilder.Entity<Produto>(builder => {
                builder.OwnsOne<Valor>(produto => produto.ValorUnitario)
                .Property(valor => valor.ValorDoItem)
                .HasColumnName("ValorDoItem");
            });

            modelBuilder.Entity<Produto>(builder => {
                builder.OwnsOne<Quantidade>(produto => produto.QuantidadeProduto)
                .Property(quantidade => quantidade.QuantidadeItem)
                .HasColumnName("QuantidadeEmEstoque");
            });

            modelBuilder.Entity<Produto>(builder => {
                builder.OwnsOne<Coloracao>(produto => produto.Cor)
                .Property(cor => cor.Cor)
                .HasColumnName("Coloracao");
            });
        }
    
        public DbSet<Produto> Produtos { get; set; }
    }
}