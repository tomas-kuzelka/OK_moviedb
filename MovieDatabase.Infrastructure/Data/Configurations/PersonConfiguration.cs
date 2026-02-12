using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.Country)
            .IsRequired()
            .HasMaxLength(50);

        // uložení enumu jako string
        builder.Property(p => p.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.Biography)
            .IsRequired()
            .HasMaxLength(2000);


        // definice Complex Property pro Address
        builder.ComplexProperty(p => p.Address, address =>
        {
            address.Property(a => a.Street)
            .HasColumnName("Street")
                .HasMaxLength(100);
            address.Property(a => a.City)
            .HasColumnName("City")
                .HasMaxLength(50);
            address.Property(a => a.Zip)
            .HasColumnName("Zip")
            .HasMaxLength(20);
        });

    }
}
