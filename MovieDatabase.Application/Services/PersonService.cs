using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.DTOs.Movie;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Application.Interfaces.Services;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Services;

public class PersonService(
    IPersonRepository personRepository,
    IMovieRepository movieRepository
    ) : IPersonService
{
    public Task<IEnumerable<PersonResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var query = personRepository.GetQueryable();

        query = query
            .Include(p => p.MoviesAsActor)
            .Include(p => p.MoviesAsDirector);

        var entities = query.ToList();

        return Task.FromResult(entities.Select(MapToResponse));
    }

    public async Task<PersonResponse> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await personRepository
            .GetQueryable()
            .Include(p => p.MoviesAsActor)
            .Include(p => p.MoviesAsDirector)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (entity == null) return null;

        return MapToResponse(entity);
    }
    public async Task<PersonResponse> CreateAsync(CreatePersonRequest dto, CancellationToken ct = default)
    {
        var entity = new Person
        {
            Name = dto.Name,
            BirthDate = dto.BirthDate,
            Country = dto.Country,
            Biography = dto.Biography,
            Role = dto.Role,
            Address = new Address(dto.Address.Street, dto.Address.City, dto.Address.Zip)
        };
        
        if (dto.MoviesAsActor.Count > 0)
        {
            var movies = await movieRepository.GetQueryable()
                .Where(m => dto.MoviesAsActor.Contains(m.Id)).ToListAsync();
            entity.MoviesAsActor = movies;
        }

        await personRepository.AddAsync(entity, ct);
        await personRepository.SaveChangesAsync(ct);

        return MapToResponse(entity);
    }


    public async Task UpdateAsync(int id, CreatePersonRequest dto, CancellationToken ct = default)
    {
        var entity = await personRepository.GetByIdAsync(id, ct);

        if (entity == null) throw new KeyNotFoundException($"Person with id {id} not found");

        entity.Name = dto.Name;
        entity.BirthDate = dto.BirthDate;
        entity.Biography = dto.Biography;
        entity.Role = dto.Role;
        entity.Country = dto.Country;

        entity.Address = new Address(dto.Address.Street, dto.Address.City, dto.Address.Zip);

        await personRepository.SaveChangesAsync(ct);

    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var entity = await personRepository.GetByIdAsync(id, ct);
        if (entity != null)
        {
            personRepository.Delete(entity);
            await personRepository.SaveChangesAsync(ct);
        }
    }

    private PersonResponse MapToResponse(Person entity)
    {
        return new PersonResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            BirthDate = entity.BirthDate,
            Country = entity.Country,
            Address = new AddressDTO(entity.Address.Street, entity.Address.City, entity.Address.Zip),
            Role = entity.Role,
            Biography = entity.Biography,
            MoviesAsActor = entity.MoviesAsActor.Select(m => new MovieSummaryDTO(m.Title, m.ReleaseDate, m.IsAvailable)).ToList(),
            MoviesAsDirector = entity.MoviesAsDirector.Select(m => new MovieSummaryDTO(m.Title, m.ReleaseDate, m.IsAvailable)).ToList()
        };
    }
}
