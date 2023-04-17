﻿using Cabanoss.Core.Common;
using System.Linq.Expressions;

namespace Cabanoss.Core.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEentity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetFirstAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}