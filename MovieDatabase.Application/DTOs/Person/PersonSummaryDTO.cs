using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.DTOs.Person;

public record PersonSummaryDTO
(
    string? Name,
    DateOnly BirthDate,
    string? Country,
    AddressDTO Address,
    PersonRole Role ,
    string? Biography 
);
