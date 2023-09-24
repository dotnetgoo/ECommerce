using AutoMapper;
using ECommerce.Api.Customers.Data;
using ECommerce.Api.Customers.Data.Entities;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly ILogger<ICustomersProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<ICustomersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private async void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.AddRange(new Customer[] 
                {
                    new Customer { Id = 1, Name = "Muhammadkarim To'xtaboyev", Address = "Yunusobod 14" },
                    new Customer { Id = 2, Name = "Samandar Uralov", Address = "Yunusobod 12" },
                    new Customer { Id = 3, Name = "Javohir G'ulomov", Address = "Yunusobod 14" },
                    new Customer { Id = 4, Name = "Islombek To'xtamurodov", Address = "Yunusobod 14" },
                });

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<(bool IsSuccess, CustomerDto Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                _logger.LogInformation("Quering customerss");

                var customer = await _dbContext.Customers.FirstOrDefaultAsync(o => o.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<Customer, CustomerDto>(customer);

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerDto> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return (false, null, ex.Message);
            }
        }
    }
}
