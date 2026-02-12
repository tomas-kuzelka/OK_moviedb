using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Genre;
using MovieDatabase.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Services;

public interface IGenreService
{
    Task<GenreResponse> CreateAsync(CreateGenreRequest dto, CancellationToken ct = default);
    Task<PageResult<GenreResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);
    Task<GenreResponse> UpdateAsync(int id, CreateGenreRequest dto, CancellationToken ct = default);
    Task<GenreResponse> GetByIdAsync(int id, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
}
