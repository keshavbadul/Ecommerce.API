using System;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.DbContexts
{
	public class EcommerceDbContext : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Category> Categories { get; set; } = null!;
		public DbSet<Product> Products { get; set; } = null!;
		public DbSet<Cart> Carts { get; set; } = null!;
		public DbSet<CartItem> CartItems { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;
		public DbSet<OrderItem> OrderItems { get; set; } = null!;

		public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
			: base(options)
		{

		}
	}
}

