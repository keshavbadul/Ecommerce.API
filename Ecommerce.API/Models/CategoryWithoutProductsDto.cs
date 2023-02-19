using System;
namespace Ecommerce.API.Models
{
	public class CategoryWithoutProductsDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}

