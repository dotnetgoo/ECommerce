namespace ECommerce.Api.Orders.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
