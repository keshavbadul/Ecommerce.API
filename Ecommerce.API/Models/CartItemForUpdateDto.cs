using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models
{
    public class CartItemForUpdateDto
    {
        [Required]
        public int Quantity { get; set; }
    }
}