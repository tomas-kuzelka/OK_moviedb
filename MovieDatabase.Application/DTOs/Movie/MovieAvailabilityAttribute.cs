using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieDatabase.Application.DTOs.Movie;

public class MovieAvailabilityAttribute(int maxYear) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is CreateMovieRequest movie && movie.AvailableFrom > new DateOnly(maxYear, 12, 31))
        {
            return new ValidationResult($"Dostupnsot nemůže být v budoucnosti, {maxYear}.", new[] { nameof(CreateMovieRequest.AvailableFrom) });
        }
        return ValidationResult.Success;
    }
}
