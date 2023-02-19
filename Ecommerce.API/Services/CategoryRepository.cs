using System;
using Ecommerce.API.DbContexts;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceDbContext _context;

        public CategoryRepository(EcommerceDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public Task AddProductToCategory(int categoryId, Product product)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        public Task<Product?> GetProductFromCategory(int categoryId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsFromCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}

