using System.Threading.Tasks;
using PortfolioGallery.API.Core.Models;

namespace PortfolioGallery.API.Core.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
    }
}