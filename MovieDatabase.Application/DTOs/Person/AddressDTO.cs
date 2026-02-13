using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieDatabase.Application.DTOs.Person;

public record AddressDTO
(
    string Street,
    string City,
    string Zip
);


