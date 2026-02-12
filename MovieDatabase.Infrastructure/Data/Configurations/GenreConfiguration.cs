using Microsoft.EntityFrameworkCore;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);
            
        builder.HasIndex(g => g.Name).IsUnique();

    }
}
