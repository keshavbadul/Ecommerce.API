using Ecommerce.API.Entities;

namespace Ecommerce.API.Services
{
    public interface IAuthenticationRepository
    {
        User ValidateUserCredentials(string userName, string password);
        void CreateUser(User user);
        Task<bool> SaveChangesAsync();
    }
}