using System.Threading.Tasks;
using PortfolioGallery.API.Core;

namespace PortfolioGallery.API.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortfolioGalleryDbContext context;

        public UnitOfWork(PortfolioGalleryDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CompleteAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}