using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieDatabase.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int ReleaseDate { get; set; }
    public bool IsAvailable { get; set; }
    // public DateOnly DateAdded { get; set; } - Shadow Property
    public int? DirectorId { get; set; }
    public Person? Director { get; set; }
    public List<Person> Actors { get; set; } = [];
    public List<Genre> Genres { get; set; } = [];
}
