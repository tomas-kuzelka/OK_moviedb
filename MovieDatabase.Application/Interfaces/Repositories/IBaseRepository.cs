using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Repositories;

public interface IBaseRepository<T, in TKey> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);

    Task<T?> GetByIdAsync(TKey id, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);

    void Update(T entity);

    void Delete(T entity);

    Task SaveChangesAsync(CancellationToken ct = default);

    IQueryable<T> GetQueryable();
}
