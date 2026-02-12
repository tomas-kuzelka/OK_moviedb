using MovieDatabase.Application.DTOs.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieDatabase.Application.DTOs.Movie;

// variant 1) - custom validation attribute
[MovieAvailability(2027)]
public record CreateMovieRequest : IValidatableObject
{
    [Display(Name = "Název", Description = "Název filmu")]
    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(200, ErrorMessage = "{0} nesmí mít více než {1} znaků.")]
    public required string Title { get; set; }
    [Display(Name = "Rok vydání")]
    [Range(1900, 2100, ErrorMessage = "{0} musí být mezi {1} a {2}.")]
    public int ReleaseDate { get; set; }
    [Display(Name = "Dostupnost")]
    public bool IsAvailable { get; set; }
    [Display(Name = "Režisér", Description = "Jméno režiséra filmu")]
    [Required(ErrorMessage = "{0} je povinné.")]
    public int? DirectorId { get; set; }

    public DateOnly AvailableFrom { get; set; }
    public IList<int> GenreIds { get; set; } = [];
    public IList<int> ActorIds { get; set; } = [];

    // variant 2) - IValidatableObject
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AvailableFrom.Year > DateTime.Now.Year)
        {
            yield return new ValidationResult("Dostupnost filmu nesmí být v budoucnosti", [nameof(AvailableFrom)]);
        }
    }
}
