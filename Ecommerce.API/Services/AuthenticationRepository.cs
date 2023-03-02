using Ecommerce.API.DbContexts;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Services
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly EcommerceDbContext _context;

        public AuthenticationRepository(EcommerceDbContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context));
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public User ValidateUserCredentials(string userName, string password)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName
				&& u.Password == password);

			// return res;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}