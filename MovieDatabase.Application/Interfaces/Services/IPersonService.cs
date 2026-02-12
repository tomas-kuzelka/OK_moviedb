using MovieDatabase.Application.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Services;

public interface IPersonService
{
    Task<PersonResponse> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<PersonResponse>> GetAllAsync(CancellationToken ct = default);

    Task <PersonResponse> CreateAsync(CreatePersonRequest dto, CancellationToken ct = default);

    Task UpdateAsync(int id, CreatePersonRequest dto, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
}
