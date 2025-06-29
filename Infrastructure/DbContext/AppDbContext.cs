 using Microsoft.EntityFrameworkCore;
using OrderStockManagement.Domain.Entities;

namespace Infrastructure.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet tanımlamaları
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

 
    }
}

