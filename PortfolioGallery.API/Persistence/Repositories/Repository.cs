using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortfolioGallery.API.Core.Repositories;

namespace PortfolioGallery.API.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PortfolioGalleryDbContext context;

        public Repository(PortfolioGalleryDbContext context)
        {
            this.context = context;
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async virtual Task<T> Get(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}