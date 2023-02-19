using System;
using Ecommerce.API.DbContexts;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Services
{
	public class ProductRepository : IProductRepository
	{
        private readonly EcommerceDbContext _context;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task AddProductToCategory(int categoryId, Product product)
        {
            var category = await GetCategoryAsync(categoryId, false);
            if (category != null)
            {
                category.Products.Add(product);
            }
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public Task<bool> CategoryNameMatchesCategoryId(string? categoryName, int categoryId)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(int categoryId, bool includeProducts)
        {
            if (includeProducts)
            {
                return await _context.Categories
                        .Include(c => c.Products)
                        .Where(c => c.Id == categoryId)
                        .FirstOrDefaultAsync();
            }

            return await _context.Categories
                    .Where(c => c.Id == categoryId)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsInCategoryAsync(
            int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();
        }
    }
}

