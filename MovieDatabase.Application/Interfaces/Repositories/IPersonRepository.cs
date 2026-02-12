using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces.Repositories;

public interface IPersonRepository : IBaseRepository<Person, int>
{

}
