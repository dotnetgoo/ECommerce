using AutoMapper;
using ECommerce.Api.Customers.Data.Entities;
using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
