using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class ProductsContext : DbContext
    {
        private readonly IConfiguration Config;

        public ProductsContext(DbContextOptions<ProductsContext> options, IConfiguration config) : base(options)
        {
            Config = config;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Config.GetValue<string>("SchemaName"));

            modelBuilder.Entity<Product>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
