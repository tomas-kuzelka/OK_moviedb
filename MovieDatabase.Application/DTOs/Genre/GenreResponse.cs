using MovieDatabase.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.DTOs.Genre;

public record GenreResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<MovieSummaryDTO> Movies { get; set; } = [];
}
