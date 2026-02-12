using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Genre;
using MovieDatabase.Application.Interfaces;
using MovieDatabase.Application.Interfaces.Services;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Services;

public class GenreService(IUnitOfWork unitOfWork, IMapper mapper) : IGenreService
{
    public async Task<PageResult<GenreResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var query = unitOfWork.GenreRepository.GetQueryable()
            .ProjectTo<GenreResponse>(mapper.ConfigurationProvider);

        var totalItems = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageResult<GenreResponse>(items, pageNumber, pageSize, totalItems);
    }

    public async Task<GenreResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await unitOfWork.GenreRepository.GetByIdAsync(id, ct);
        if (entity == null) throw new KeyNotFoundException($"Genre with id {id} not found");

        return mapper.Map<GenreResponse>(entity);
    }

    public async Task<GenreResponse> CreateAsync(CreateGenreRequest dto, CancellationToken ct = default)
    {
        var entity = mapper.Map<Genre>(dto);

        // načtení a přiřazení filmů (Movies)
        if (dto.MovieIds.Count > 0)
        {
            var movies = await unitOfWork.MovieRepository.GetQueryable()
                .Where(m => dto.MovieIds.Contains(m.Id))
                .ToListAsync(ct);
            entity.Movies = movies;
        }

        await unitOfWork.GenreRepository.AddAsync(entity, ct);
        await unitOfWork.CommitAsync(ct);

        var createdEntity = await unitOfWork.GenreRepository.GetWithMoviesAsync(entity.Id, ct);

        if (createdEntity == null)
            throw new InvalidOperationException("Failed to retrieve created movie");

        return mapper.Map<GenreResponse>(entity);

    }
    public async Task<GenreResponse> UpdateAsync(int id, CreateGenreRequest dto, CancellationToken ct = default)
    {
        var entity = await unitOfWork.GenreRepository.GetByIdAsync(id, ct);

        if (entity == null) throw new KeyNotFoundException($"Genre with id {id} not found");

        mapper.Map(dto, entity);

        entity.Movies.Clear();
        if (dto.MovieIds.Count > 0)
        {
            var movies = await unitOfWork.MovieRepository.GetQueryable()
                .Where(m => dto.MovieIds.Contains(m.Id))
                .ToListAsync(ct);
            foreach (var movie in movies)
                entity.Movies.Add(movie);
        }

        await unitOfWork.CommitAsync(ct);

        return mapper.Map<GenreResponse>(entity);
    } 

    
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await unitOfWork.GenreRepository.GetByIdAsync(id, ct);
        if (entity == null) throw new KeyNotFoundException($"Genre with id {id} not found");

        unitOfWork.GenreRepository.Delete(entity);
        await unitOfWork.CommitAsync(ct);
    }

   
}
