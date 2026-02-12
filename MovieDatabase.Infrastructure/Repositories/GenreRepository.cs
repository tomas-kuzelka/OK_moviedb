using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Domain.Entities;
using MovieDatabase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Repositories;

public class GenreRepository : BaseRepository<Genre, int>, IGenreRepository
{
    public GenreRepository(MoviesDbContext context) : base(context)
    {
    }

    public async Task<Genre?> GetWithMoviesAsync(int id, CancellationToken
         ct = default)
    {
        return await _dbSet
            .Include(g => g.Movies)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

}
