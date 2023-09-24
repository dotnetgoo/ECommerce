using AutoMapper;
using ECommerce.Api.Orders.Data;
using ECommerce.Api.Orders.Data.Entities;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<IOrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<IOrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger =logger;
            this.mapper =mapper;

            SeedData();
        }

        private async void SeedData()
        {
            if(!dbContext.OrderItems.Any()) 
            {
                dbContext.OrderItems.AddRange(new OrderItem[]
                {
                    new OrderItem { Id = 1, OrderId = 1, ProductId = 2, Quantity = 3, UnitPrice = 540 },
                    new OrderItem { Id = 2, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 240 },
                    new OrderItem { Id = 3, OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 140 },
                });
                await dbContext.SaveChangesAsync();
            }


            // add seed data to orders
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.AddRange(new Order[]
                {
                    new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.UtcNow, Total = 10 }
                });

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderDto> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                
                return (false, null, ex.Message);
            }
        }
    }
}
