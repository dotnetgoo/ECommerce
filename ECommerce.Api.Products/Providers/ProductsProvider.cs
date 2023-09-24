using AutoMapper;
using ECommerce.Api.Products.Data;
using ECommerce.Api.Products.Entities;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<IProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<IProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private async void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(
                    new Entities.Product[]
                    {
                        new Product
                        {
                            Id = 1,
                            Name = "Keyboard",
                            Price = 20,
                            Inventory = 100
                        },
                        new Product
                        {
                            Id = 2,
                            Name = "Mouse",
                            Price = 10,
                            Inventory = 80
                        },
                        new Product
                        {
                            Id = 3,
                            Name = "Monitor",
                            Price = 80,
                            Inventory = 90
                        },
                        new Product
                        {
                            Id = 4,
                            Name = "CPU",
                            Price = 120,
                            Inventory = 50
                        }
                    });

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
                    
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Product, ProductDto>(product);

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}
