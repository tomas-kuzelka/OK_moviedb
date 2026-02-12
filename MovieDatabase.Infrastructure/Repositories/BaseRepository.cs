using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Repositories;

public abstract class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
{
    protected readonly MoviesDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(MoviesDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync([id], ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
    }
    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        // pokud by nebyla načtena v aktuálním kontextu, tak ji připojíme, aby mohla být smazána
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }


    public IQueryable<T> GetQueryable()
    {
        return _dbSet.AsNoTracking().AsQueryable();
    }

}
