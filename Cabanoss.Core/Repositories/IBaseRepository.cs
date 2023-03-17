using Cabanoss.Core.Common;

namespace Cabanoss.Core.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEentity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetFirstAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}