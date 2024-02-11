using System.Linq.Expressions;
using Application.Data;

namespace Application.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}