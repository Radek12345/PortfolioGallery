using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PortfolioGallery.API.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        void Add(T entity);

        void Remove(T entity);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}