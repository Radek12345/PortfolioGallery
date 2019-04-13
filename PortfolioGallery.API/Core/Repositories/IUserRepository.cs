using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioGallery.API.Core.Models;

namespace PortfolioGallery.API.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserEager(int id);
    }
}