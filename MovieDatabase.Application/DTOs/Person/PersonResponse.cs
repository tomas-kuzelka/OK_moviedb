using MovieDatabase.Application.DTOs.Movie;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.DTOs.Person;

public record PersonResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Country { get; set; } = string.Empty;
    public required AddressDTO Address { get; set; } 
    public PersonRole Role { get; set; }
    public string Biography { get; set; } = string.Empty;
    public IEnumerable<MovieSummaryDTO> MoviesAsActor { get; set; } = [];
    public IEnumerable<MovieSummaryDTO> MoviesAsDirector { get; set; } = [];
}
