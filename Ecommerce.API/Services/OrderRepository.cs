using Ecommerce.API.DbContexts;
using Ecommerce.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderRepository(EcommerceDbContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context));
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void CreateOrderItemAsync(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersForUserAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order?> GetOrder(int orderId, bool includeItems)
        {
            if (includeItems)
            {
                return await _context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Where(o => o.Id == orderId)
                    .FirstOrDefaultAsync();
            }

            return await _context.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}