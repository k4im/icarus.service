using icarus.estoque.Models;
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
                .Property(produto => produto.Quantidade)
                .IsConcurrencyToken();
                
            modelBuilder.Entity<Produto>()
                .Property(produto => produto.RowVersion)
                .IsConcurrencyToken();
        }
    
        public DbSet<Produto> Produtos { get; set; }
    }
}