using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Entities
{
	public class Product
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

        [MaxLength(200)]
		public string? Description { get; set; }

		[Precision(18,2)]
		public decimal SellingPrice { get; set; }

		[Precision(18,2)]
		public decimal PurchasePrice { get; set; }

		public string? Image { get; set; }

		public int QuantityInStock { get; set; }

		[ForeignKey("CategoryId")]
		public Category? Category { get; set; }
		public int CategoryId { get; set; }

        public Product(string name)
        {
            Name = name;
        }
    }
}

