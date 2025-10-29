using Microsoft.EntityFrameworkCore;
using CepServiceApp.Models;

namespace CepServiceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cep> Ceps { get; set; }

 
    }
}