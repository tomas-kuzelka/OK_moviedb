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

}
