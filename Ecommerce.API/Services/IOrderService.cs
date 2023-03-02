namespace Ecommerce.API.Services
{
    public interface IOrderService
    {
        Task ConvertCartToOrder(int userId);
    }
}