using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Movie;
using MovieDatabase.Application.Interfaces;
using MovieDatabase.Application.Interfaces.Services;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Services;

public class MovieService(IUnitOfWork unitOfWork, IMapper mapper) : IMovieService
{
    public async Task<PageResult<MovieResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var query = unitOfWork.MovieRepository.GetQueryable()
            .ProjectTo<MovieResponse>(mapper.ConfigurationProvider);

        var totalItems = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PageResult<MovieResponse>(items, pageNumber, pageSize, totalItems);
    }

    public async Task<MovieResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await unitOfWork.MovieRepository.GetByIdAsync(id, ct);
        if (entity == null) throw new KeyNotFoundException($"Movie with id {id} not found");
        
        return mapper.Map<MovieResponse>(entity);
    }
    public async Task<MovieResponse> CreateAsync(CreateMovieRequest dto, CancellationToken ct = default)
    {
        var entity = mapper.Map<Movie>(dto);

        // načtení a přiřazení herců (Actors)
        if (dto.ActorIds.Count > 0)
        {
            var actors = await unitOfWork.PersonRepository.GetQueryable()
                .Where(p => dto.ActorIds.Contains(p.Id))
                .ToListAsync(ct);
            entity.Actors.AddRange(actors);
        }

        // načtení a přiřazení žánrů (Genres)
        if (dto.GenreIds.Count > 0)
        {
            var genres = await unitOfWork.GenreRepository.GetQueryable()
                .Where(g => dto.GenreIds.Contains(g.Id))
                .ToListAsync(ct);
            entity.Genres.AddRange(genres);
        }

        await unitOfWork.MovieRepository.AddAsync(entity, ct);
        await unitOfWork.CommitAsync(ct);

        var createdEntity = await unitOfWork.MovieRepository.GetWithDetailsAsync(entity.Id, ct);

        if (createdEntity == null)
            throw new InvalidOperationException("Failed to retrieve created movie");

        return mapper.Map<MovieResponse>(entity);
    }

    public async Task<MovieResponse> UpdateAsync(int id, CreateMovieRequest dto, CancellationToken ct = default)
    {
        var entity = await unitOfWork.MovieRepository.GetByIdAsync(id, ct);
        if (entity == null) throw new KeyNotFoundException($"Movie with id {id} not found");

        // aktualizace základním vlastností
        mapper.Map(dto, entity);

        entity.Actors.Clear();
        // načtení a přiřazení herců (Actors)
        if (dto.ActorIds.Count > 0)
        {
            var actors = await unitOfWork.PersonRepository.GetQueryable()
                .Where(p => dto.ActorIds.Contains(p.Id))
                .ToListAsync(ct);
            foreach (var actor in actors)
                entity.Actors.Add(actor);
        }

        entity.Genres.Clear();
        // načtení a přiřazení žánrů (Genres)
        if (dto.GenreIds.Count > 0)
        {
            var genres = await unitOfWork.GenreRepository.GetQueryable()
                .Where(g => dto.GenreIds.Contains(g.Id))
                .ToListAsync(ct);
            foreach (var genre in genres)
                entity.Genres.Add(genre);
        }
        
        await unitOfWork.CommitAsync(ct);

        return mapper.Map<MovieResponse>(entity);
    }
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await unitOfWork.MovieRepository.GetByIdAsync(id, ct);
        if (entity == null) throw new KeyNotFoundException($"Movie with id {id} not found");

        unitOfWork.MovieRepository.Delete(entity);
        await unitOfWork.CommitAsync(ct);
    }
}
