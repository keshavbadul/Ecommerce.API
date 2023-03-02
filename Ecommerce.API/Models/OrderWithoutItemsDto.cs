namespace Ecommerce.API.Models
{
    public class OrderWithoutItemsDto
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public decimal OrderTotal { get; set; }
        public string  OrderStatus { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
    }
}