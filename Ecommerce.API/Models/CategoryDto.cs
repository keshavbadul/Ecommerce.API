using System;
namespace Ecommerce.API.Models
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public int NumberOfProducts
		{
			get
			{
				return Products.Count;
			}
		}

		public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
	}
}

