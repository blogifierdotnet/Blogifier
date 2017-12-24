using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blogifier.Core.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> All();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
