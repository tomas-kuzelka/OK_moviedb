using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieDatabase.Application.DTOs.Person;

public record AddressDTO
(
    string? Street,
    string? City,
    string? Zip
);


//public class AddressDTO
//{
//    public string? Street { get; set; }
//    public string? City { get; set; }
//    public string? Zip { get; set; }

//    public AddressDTO(string? street, string? city, string? zip)
//    {
//        Street = street;
//        City = city;
//        Zip = zip;
//    }
//}


