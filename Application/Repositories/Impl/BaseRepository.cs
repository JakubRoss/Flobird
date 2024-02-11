using System.Linq.Expressions;
using Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.Impl
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private DatabaseContext Context { get; }
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(DatabaseContext context) 
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await _dbSet.AddAsync(entity)).Entity;
            await Context.SaveChangesAsync();

            return addedEntity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removedEntity = _dbSet.Remove(entity).Entity;
            await Context.SaveChangesAsync();

            return removedEntity;
        }
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _dbSet.Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
        public async Task BeginTransactionAsync()
        {
            await Context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await Context.SaveChangesAsync();
            await Context.Database.CurrentTransaction!.CommitAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await Context.Database.CurrentTransaction!.RollbackAsync();
        }
    }
}
