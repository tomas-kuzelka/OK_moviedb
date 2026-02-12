using Microsoft.EntityFrameworkCore;
using MovieDatabase.Domain.Entities;
using MovieDatabase.Infrastructure.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Data;

public class MoviesDbContext : DbContext
{
    public MoviesDbContext(DbContextOptions options) : base(options)
    {}

    DbSet<Movie> Movies => Set<Movie>();
    DbSet<Person> People => Set<Person>();
    DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesDbContext).Assembly);

        modelBuilder.RemoveCascadingDeletes();

       // AddTestingData(modelBuilder);
    }

    //private void AddTestingData(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Person>().HasData(
    //        new Person
    //        {
    //            Id = 1,
    //            Name = "Quentin Tarantino",
    //            Biography = "Natočil Pulp Fiction, Kill Bill, Hanebný pancharti, ...",
    //            Role = PersonRole.Director,
    //            Country = "USA",
    //            Address = new Address("Americká", "Amerika", "93893")
    //        });
    //}
}
