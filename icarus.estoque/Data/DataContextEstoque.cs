using icarus.estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace icarus.estoque.Data
{
    public class DataContextEstoque : DbContext
    {
        public DataContextEstoque(DbContextOptions options) : base(options)
        {}
    
        public DbSet<Produto> Produtos { get; set; }
    }
}