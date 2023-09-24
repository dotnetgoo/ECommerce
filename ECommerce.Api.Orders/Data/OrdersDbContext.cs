using ECommerce.Api.Orders.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
