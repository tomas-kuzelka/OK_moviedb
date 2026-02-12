using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Domain.Entities;

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Movie> Movies { get; set; } = [];
}
