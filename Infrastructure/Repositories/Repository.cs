
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Identity.Infrastructure.Repositories.Interfaces;

namespace Identity.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public Repository(AppContext appContext)
    {
        _appContext = appContext;
        _dbSet = _appContext.Set<TEntity>();
    }

    private readonly AppContext _appContext;
    private readonly DbSet<TEntity> _dbSet;

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _appContext.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _appContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _appContext.Entry(entity).State = EntityState.Modified;
        await _appContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        if (_appContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        await _appContext.SaveChangesAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        await _appContext.SaveChangesAsync();
    }
}
