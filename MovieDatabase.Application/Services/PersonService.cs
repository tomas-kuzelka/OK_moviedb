using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Movie;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Application.Interfaces;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Application.Interfaces.Services;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Services;

public class PersonService(IUnitOfWork unitOfWork, IMapper mapper) : IPersonService
{

    public async Task<PageResult<PersonResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var query = unitOfWork.PersonRepository.GetQueryable()
            .ProjectTo<PersonResponse>(mapper.ConfigurationProvider);

        var totalItems = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PageResult<PersonResponse>(items, pageNumber, pageSize, totalItems);
    }


    public async Task<PageResult<PersonResponse>> GetAllPersonsAsync(PersonRole personRole, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var query = unitOfWork.PersonRepository.GetQueryable()
            .Where(p => p.Role == personRole)
            .OrderBy(p => p.Name)
            .ProjectTo<PersonResponse>(mapper.ConfigurationProvider);

        var totalItems = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return new PageResult<PersonResponse>(items, pageNumber, pageSize, totalItems);
    }
   
    public async Task<PersonResponse> CreateAsync(CreatePersonRequest dto, CancellationToken ct = default)
    {
       var entity = mapper.Map<Person>(dto);

        // namapovat filmy (Actors)
        if (dto.MoviesAsActor.Count > 0)
        {
            var movies = await unitOfWork.MovieRepository.GetQueryable()
                .Where(m => dto.MoviesAsActor.Contains(m.Id))
                .ToListAsync(ct);
            entity.MoviesAsActor = movies;
        }

        // namapovat filmy (Directors)
        if (dto.MoviesAsDirector.Count > 0)
        {
            var movies = await unitOfWork.MovieRepository.GetQueryable()
                .Where(m => dto.MoviesAsDirector.Contains(m.Id))
                .ToListAsync(ct);
            entity.MoviesAsDirector = movies;
        }

        await unitOfWork.PersonRepository.AddAsync(entity, ct);
        await unitOfWork.CommitAsync(ct);

        return mapper.Map<PersonResponse>(entity);
    }

    public async Task<PersonResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await unitOfWork.PersonRepository.GetWithMoviesAsync(id, ct);

        if (entity is null) return null;

        return mapper.Map<PersonResponse>(entity);
    }

    public async Task<PersonResponse> UpdateAsync(int id, CreatePersonRequest dto, CancellationToken ct = default)
    {
        var entity = await unitOfWork.PersonRepository.GetWithMoviesAsync(id, ct);

        if (entity is null)
            throw new KeyNotFoundException($"Person with id {id} not found");

        mapper.Map(dto, entity);

        entity.MoviesAsActor.Clear();
        if (dto.MoviesAsActor.Count > 0)
        {
            var movies = await unitOfWork.MovieRepository.GetQueryable()
                .Where(m => dto.MoviesAsActor.Contains(m.Id))
                .ToListAsync(ct);
            foreach (var movie in movies)
                entity.MoviesAsActor.Add(movie);
        }

        entity.MoviesAsActor.Clear();
        if (dto.MoviesAsDirector.Count > 0)
        {
            var movies = await unitOfWork.MovieRepository.GetQueryable()
                .Where(m => dto.MoviesAsDirector.Contains(m.Id))
                .ToListAsync(ct);
            foreach (var movie in movies)
                entity.MoviesAsDirector.Add(movie);
        }
        await unitOfWork.CommitAsync(ct);

        return mapper.Map<PersonResponse>(entity);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await unitOfWork.PersonRepository.GetByIdAsync(id, ct);
        if (entity == null) 
            throw new KeyNotFoundException($"Person with id {id} not found");

        unitOfWork.PersonRepository.Delete(entity);
        await unitOfWork.CommitAsync(ct);
    }
}
