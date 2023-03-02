using System;

namespace Ecommerce.API.Models
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
    }
}