using MovieDatabase.Application.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieDatabase.Application.DTOs.Genre;

public record CreateGenreRequest
{
    [Display(Name = "Žánr", Description = "Název žánru")]
    [Required(ErrorMessage = "{0} je povinný údaj")]
    public required string Name { get; set; } = string.Empty;
    public IList<int> MovieIds { get; set; } = [];
}
