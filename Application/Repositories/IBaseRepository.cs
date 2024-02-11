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
        Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}