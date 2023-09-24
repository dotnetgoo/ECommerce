using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;

        public SearchService(IOrderService orderService)
        {
            _orderService=orderService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var ordersResult = await _orderService.GetOrdersAsync(customerId);
            if(ordersResult.IsSuccess)
            {
                var result = new
                {
                    Orders = ordersResult.Orders,
                };

                return (true, result);
            }

            return (false, null);
        }
    }
}
