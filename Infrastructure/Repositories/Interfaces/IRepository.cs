using System.Linq.Expressions;

namespace Identity.Infrastructure.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities);
}
