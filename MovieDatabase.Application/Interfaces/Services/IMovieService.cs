using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Movie;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Services;

public interface IMovieService
{
    Task<MovieResponse> CreateAsync(CreateMovieRequest dto, CancellationToken ct = default);
    Task<PageResult<MovieResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
    Task<MovieResponse> UpdateAsync(int id, CreateMovieRequest dto, CancellationToken ct = default);
    Task<MovieResponse> GetByIdAsync(int id, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
}
