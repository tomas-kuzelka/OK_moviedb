using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieDatabase.Domain.Entities;

[ComplexType]
public record Address(
    string? Street,
    string? City,
    string? Zip
);
