using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _orderService;
        private readonly IProductsService _productService;
        private readonly ICustomersService _customerService;

        public SearchService(IOrdersService orderService, IProductsService productService, ICustomersService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var ordersResult = await _orderService.GetOrdersAsync(customerId);
            var productsResult = await _productService.GetProductsAsync();
            var customersResult = await _customerService.GetCustomerAsync(customerId);

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess
                            ? productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                            : "Product information is not available";
                    }
                }
                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                                customersResult.Customer :
                                new { Name = "Customer information is not available" },
                    Orders = ordersResult.Orders,
                };

                return (true, result);
            }

            return (false, null);
        }
    }
}
