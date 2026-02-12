using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Domain.Entities;
using MovieDatabase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person, int>, IPersonRepository
{
    public PersonRepository(MoviesDbContext context) : base(context)
    {
    }



}

