using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PortfolioGallery.API.Controllers.Resources;
using PortfolioGallery.API.Core.Models;

namespace PortfolioGallery.API.Core.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(LoginResource userResource);
        string CreateToken(User user);
    }
}