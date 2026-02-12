using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MovieDatabase.Domain.Entities;

public enum PersonRole
{
    Actor,
    Director

}

public class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public required string Country { get; set; }
    public required Address Address { get; set; } // Complex Type
    public PersonRole Role { get; set; }
    public required string Biography { get; set; }
    public ICollection<Movie> MoviesAsActor { get; set; } = [];
    public ICollection<Movie> MoviesAsDirector { get; set; } = [];
}
