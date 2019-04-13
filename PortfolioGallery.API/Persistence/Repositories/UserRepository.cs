using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortfolioGallery.API.Core.Models;
using PortfolioGallery.API.Core.Repositories;

namespace PortfolioGallery.API.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PortfolioGalleryDbContext context) : base(context) {}

        public async Task<User> GetUserEager(int id)
        {
            return await context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}