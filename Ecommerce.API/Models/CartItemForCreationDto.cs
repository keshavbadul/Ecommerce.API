using System;

namespace Ecommerce.API.Models
{
    public class CartItemForCreationDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}