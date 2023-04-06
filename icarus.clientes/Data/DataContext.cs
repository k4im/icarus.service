using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.clientes.Models;
using Microsoft.EntityFrameworkCore;

namespace icarus.clientes.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}