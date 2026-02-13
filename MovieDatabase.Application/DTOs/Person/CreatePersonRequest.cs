using MovieDatabase.Application.DTOs.Movie;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieDatabase.Application.DTOs.Person;

public record CreatePersonRequest // : IValidatableObject
{
    [Display(Name = "Jméno")]
    [Required(ErrorMessage = "{0} je povinné.")]
    [StringLength(100, ErrorMessage = "{0} může mít maximálně {1} znaků")]
    public required string Name { get; set; }
    [Display(Name = "Datum narození")]
    [Required(ErrorMessage = "{0} je povinné.")]
    public DateOnly BirthDate { get; set; }
    [Display(Name = "Země")]
    [Required(ErrorMessage = "{0} je povinné.")]

    public required AddressDTO Address { get; set; }
    public required string Country { get; set; }
  
    public PersonRole Role { get; set; }
    [Display(Name = "Biografie")]
    [Required(ErrorMessage = "{0} je povinné.")]
    [DataType(DataType.MultilineText)]
    public required string Biography { get; set; }
    public IList<int> MoviesAsActor { get; set; } = [];
    public IList<int> MoviesAsDirector { get; set; } = [];

}
