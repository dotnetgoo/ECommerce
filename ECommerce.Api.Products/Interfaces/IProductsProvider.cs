using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, ProductDto Products, string ErrorMessage)> GetProductAsync(int id);
    }
}
