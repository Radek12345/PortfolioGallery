using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PortfolioGallery.API.Controllers.Resources;
using PortfolioGallery.API.Core.Models;
using PortfolioGallery.API.Core.Repositories;
using PortfolioGallery.API.Core.Services;

namespace PortfolioGallery.API.Core.ServicesImplementations
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> repo;
        private readonly IUnitOfWork unit;

        public AuthService(IRepository<User> repo, IUnitOfWork unit)
        {
            this.unit = unit;
            this.repo = repo;
        }

        public async Task<User> Register(User user, string password)
        {
            using(var hmac = new HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            user.Email = user.Email.ToLower();
            user.Name = user.Name.ToLower();
            user.Created = DateTime.Now;
            
            repo.Add(user);
            await unit.CompleteAsync();

            return user;
        }

        public async Task<User> Login(UserResource userResource)
        {
            User user;

            if (userResource.Email != null)
                user = await repo.FirstOrDefault(u => u.Email == userResource.Email.ToLower());
            else
                user = await repo.FirstOrDefault(u => u.Name == userResource.Name.ToLower());

            if (user == null)
                return null;

            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userResource.Password));
                if (!user.PasswordHash.SequenceEqual(computedHash))
                    return null;
            }

            return user;
        }
    }
}