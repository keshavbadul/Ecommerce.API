using Ecommerce.API.Entities;

namespace Ecommerce.API.Services
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrder(int orderId, bool includeItems);
        Task<IEnumerable<Order>> GetAllOrdersForUserAsync(int userId);
        void CreateOrder(Order order);
        void CreateOrderItemAsync(OrderItem orderItem);
        Task<bool> SaveChangesAsync();
    }
}