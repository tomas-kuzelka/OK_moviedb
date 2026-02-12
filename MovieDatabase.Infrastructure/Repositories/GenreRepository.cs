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

}
