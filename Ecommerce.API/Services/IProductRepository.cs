using System;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Services
{
	public interface IProductRepository
	{
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int categoryId, bool includeProducts);
        Task CreateCategoryAsync(Category category);
        Task<bool> CategoryExistsAsync(int categoryId);
        Task<Product?> GetProductAsync(int productId);
        Task AddProductToCategory(int categoryId, Product product);
        void DeleteProduct(Product product);
        Task<bool> CategoryNameMatchesCategoryId(string? categoryName,
            int categoryId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsInCategoryAsync(int categoryId);
        Task<bool> SaveChangesAsync();
    }
}

