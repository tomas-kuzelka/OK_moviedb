using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Repositories;

public interface IPersonRepository : IBaseRepository<Person, int>
{
    Task<Person?> GetWithMoviesAsync(int id, CancellationToken td = default);


    Task<IEnumerable<Person>> GetAllWithMoviesAsync(int pageNumber = 1, int pageSize = 10, CancellationToken ct = default);

    Task<IEnumerable<Person>> GetAllPersonsAsync(PersonRole personRole, int pageNumbe = 1, int pageSize = 10, CancellationToken ct = default);
}
