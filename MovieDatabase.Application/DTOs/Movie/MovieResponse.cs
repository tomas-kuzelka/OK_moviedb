using MovieDatabase.Application.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.DTOs.Movie;

public record MovieResponse
{
    public int Id { get; set; }    
    public string Title { get; set; } = string.Empty;
    public int ReleaseDate { get; set; }
    public bool IsAvailable { get; set; }
    public string DirectorName { get; set; } = string.Empty;
    public IEnumerable<string> Genres { get; set; } = [];
    public IEnumerable<PersonSummaryDTO> Actors { get; set; } = [];
}
