using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.ReleaseDate)
            .IsRequired();

        // definice Shadow Property pro DateAdded
        builder.Property<DateOnly>("DateAdded")
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()");

        // one-to-many: režisér a filmy
        builder.HasOne(m => m.Director)
            .WithMany(d => d.MoviesAsDirector)
            .HasForeignKey(m => m.DirectorId);

        // many-to-many: filmy a herci
        builder.HasMany(m => m.Actors)
            .WithMany(a => a.MoviesAsActor)
            .UsingEntity(j => j.ToTable("MovieActors"));
    }
}
