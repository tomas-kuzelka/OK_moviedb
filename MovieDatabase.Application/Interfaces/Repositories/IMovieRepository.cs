using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Repositories;

public interface IMovieRepository : IBaseRepository<Movie, int>
{

    Task<Movie?> GetWithDetailsAsync(int id, CancellationToken ct = default);
}


