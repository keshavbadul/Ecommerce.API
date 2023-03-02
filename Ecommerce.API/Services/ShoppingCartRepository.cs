using System;
using Ecommerce.API.DbContexts;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly EcommerceDbContext _context;

        public ShoppingCartRepository(EcommerceDbContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context));
        }

        public Task ChangeQuantityOfItemInCartAsync(int cartId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckIfUserHasCartAsync(int userId)
        {
            return await _context.Carts.Where(c => c.UserId == userId).AnyAsync();
        }

        // public async Task<Cart> GetUsersCart(int userId)
        // {
        //     return await _context.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();
        // }

        public async Task<IEnumerable<Product?>> GetAllProductsInCartAsync(int userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .SelectMany(c => c.CartItems)
                .Include(ci => ci.Product)
                .Select(ci => ci.Product)
                .ToListAsync();
        }

        public void CreateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
        }

        public void CreateShoppingCart(int userId)
        {
            _context.Carts.Add(new Cart()
            {
                UserId = userId
            });
        }

        public async Task<CartItem?> GetCartItem(int cartItemId)
        {
            return await _context.CartItems
                .Where(ci => ci.Id == cartItemId)
                .FirstOrDefaultAsync();
        }

        // public IEnumerable<CartItem> GetCartItems(Cart cart)
        // {
        //     return cart.CartItems;
        // }

        public void DeleteCartItem(CartItem cartItem)
        {
            _context.CartItems.Remove(cartItem);
        }

        public void ClearCart(int userId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);

            if (cart != null)
            {
                var cartItems = _context.CartItems.Where(ci => ci.CartId == cart.Id);

                _context.CartItems.RemoveRange(cartItems);
            }
        }

        public Task<IEnumerable<Cart>> GetShoppingCartsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Cart?> GetUserCart(int userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}