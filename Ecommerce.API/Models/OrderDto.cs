namespace Ecommerce.API.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public decimal OrderTotal { get; set; }
        public string  OrderStatus { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } =
            new List<OrderItemDto>();
    }
}