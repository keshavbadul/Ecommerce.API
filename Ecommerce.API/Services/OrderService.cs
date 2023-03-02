using Ecommerce.API.Entities;
using Ecommerce.API.Exceptions;

namespace Ecommerce.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _cartRepository;

        public OrderService(
            IOrderRepository orderRepository, 
            IShoppingCartRepository cartRepository)
        {
            _orderRepository = orderRepository ?? 
                throw new ArgumentNullException(nameof(orderRepository));
            _cartRepository = cartRepository ?? 
                throw new ArgumentNullException(nameof(cartRepository));
        }

        public async Task ConvertCartToOrder(int userId)
        {
            var cart = await _cartRepository.GetUserCart(userId);

            if (cart == null)
            {
                throw new CartNotFoundException();
            }

            var cartItems = cart.CartItems;

            if (cartItems.Count == 0)
            {
                throw new CartEmptyException();
            }

            var orderItems = new List<OrderItem>();

            decimal orderTotal = 0;

            foreach (var cartItem in cartItems)
            {
                orderItems.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                });

                if (cartItem.Product != null) 
                {
                    orderTotal += cartItem.Product.SellingPrice;
                }
            }

            var order = new Order
            {
                UserId = userId,
                OrderItems = orderItems,
                OrderStatus = "Pending",
                OrderTotal = orderTotal
            };

            _orderRepository.CreateOrder(order);

            _cartRepository.ClearCart(userId);
        }

        // private string GenerateOrderNumber()
        // {
        //     // Implementation to generate order number
        // }
    }

}