using System;
namespace Ecommerce.API.Models
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public decimal SellingPrice { get; set; }
		public string? Image { get; set; }
    }
}
