using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public virtual void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public virtual void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async virtual Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

    }
}