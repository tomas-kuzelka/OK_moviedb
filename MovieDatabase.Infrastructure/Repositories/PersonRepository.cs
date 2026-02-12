using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Domain.Entities;
using MovieDatabase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person, int>, IPersonRepository
{
    public PersonRepository(MoviesDbContext context) : base(context)
    {
    }
    public async Task<Person?> GetWithMoviesAsync(int id, CancellationToken td = default)
    {
        return await _dbSet
            .Include(p => p.MoviesAsDirector)
            .Include(p => p.MoviesAsActor)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Person>> GetAllWithMoviesAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
    {
        return await _dbSet.AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.MoviesAsDirector)
            .Include(p => p.MoviesAsActor)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync(PersonRole personRole, int pageNumbe = 1, int pageSize = 10, CancellationToken ct = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(p => p.Role == personRole)
            .OrderBy(p => p.Name)
            .Skip((pageNumbe - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }

}

