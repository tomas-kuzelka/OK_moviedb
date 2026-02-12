using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Domain.Entities;
using MovieDatabase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Repositories;

public class MovieRepository : BaseRepository<Movie, int>, IMovieRepository
{
    public MovieRepository(MoviesDbContext context) : base(context)
    {
    }


    public async Task<Movie?> GetWithDetailsAsync(int id, CancellationToken ct = default)
    {
        return await _dbSet
            .Include(m => m.Genres)
            .Include(m => m.Actors)
            .Include(m => m.Director)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

}
