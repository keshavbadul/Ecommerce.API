using System;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Services
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<Cart>> GetShoppingCartsAsync();
        Task<Cart?> GetUserCart(int userId);
        void CreateShoppingCart(int userId);
        Task<bool> CheckIfUserHasCartAsync(int userId);
        // Task<Cart> GetUsersCart(int userId);
        Task<IEnumerable<Product?>> GetAllProductsInCartAsync(int userId);
        void CreateCartItemAsync(CartItem cartItem);
        Task ChangeQuantityOfItemInCartAsync(int cartId, int productId, int quantity);
        Task<CartItem?> GetCartItem(int cartItemId);
        void DeleteCartItem(CartItem cartItemId);
        // IEnumerable<CartItem> GetCartItems(Cart cart);
        void ClearCart(int userId);
        Task<bool> SaveChangesAsync();
    }
}