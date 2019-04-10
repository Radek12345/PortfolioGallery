using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration config;

        public AuthService(IRepository<User> repo, IUnitOfWork unit, IConfiguration config)
        {
            this.config = config;
            this.unit = unit;
            this.repo = repo;
        }

        public async Task<User> Register(User user, string password)
        {
            using (var hmac = new HMACSHA512())
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

        public async Task<User> Login(LoginResource userResource)
        {
            var login = userResource.Login.ToLower();
            var user = await repo.FirstOrDefault(u => u.Email == login || u.Name == login);

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

        public string CreateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(CreateClaims(user)),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private Claim[] CreateClaims(User user)
        {
            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
        }
    }
}