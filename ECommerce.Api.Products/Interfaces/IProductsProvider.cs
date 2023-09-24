using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
