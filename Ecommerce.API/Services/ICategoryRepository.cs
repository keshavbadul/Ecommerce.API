using System;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Services
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetCategoriesAsync();
		Task<Category?> GetCategoryAsync(int categoryId, bool includeProducts);
		Task<bool> CategoryExistsAsync(int categoryId);
		Task<IEnumerable<Product>> GetProductsFromCategoryAsync(int categoryId);
		Task<Product?> GetProductFromCategory(int categoryId, int productId);
		Task AddProductToCategory(int categoryId, Product product);
		void DeleteProduct(Product product);
		Task<bool> CategoryNameMatchesCategoryId(string? categoryName,
			int categoryId);
		Task<bool> SaveChangesAsync();
	}
}

