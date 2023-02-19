using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models
{
	public class ProductForUpdateDto
	{
		[Required]
		[MaxLength(20)]
		public string Name { get; set; } = string.Empty;
		[MaxLength(200)]
		public string? Description { get; set; }
		public decimal PurchasePrice { get; set; }
		public decimal SellingPrice { get; set; }
		public string? Image { get; set; }
		public int QuantityInStock { get; set; }
		public int CategoryId { get; set; }
	}
}

