using System;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.DbContexts
{
	public class EcommerceDbContext : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }

		public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
			: base(options)
		{

		}
	}
}

