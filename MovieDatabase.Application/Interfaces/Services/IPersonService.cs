using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Services;

public interface IPersonService
{
    Task<PageResult<PersonResponse>> GetAllPersonsAsync(PersonRole personRole, int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
    Task <PersonResponse> CreateAsync(CreatePersonRequest dto, CancellationToken ct = default);

    Task<PageResult<PersonResponse>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default)
}
