using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Entities
{
	public class Category
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(50)]
		[Required]
		public string Name { get; set; }

		[MaxLength(200)]
		public string? Description { get; set; }

		public ICollection<Product> Products { get; set; } = new
			List<Product>();

		public Category(string name)
        {
            Name = name;
        }
    }
}

