using ECommerce.Api.Customers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Data
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions options) 
            : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
