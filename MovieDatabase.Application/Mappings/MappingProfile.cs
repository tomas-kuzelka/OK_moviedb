using AutoMapper;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        // Person Mappings
        CreateMap<Person, PersonResponse>();

        CreateMap<Person, PersonSummaryDTO>();

        CreateMap<CreatePersonRequest, Person>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(src.Address.Street, src.Address.City, src.Address.Zip)))
            .ForMember(dest => dest.MoviesAsActor, opt => opt.Ignore())
            .ForMember(dest => dest.MoviesAsDirector, opt => opt.Ignore());

        CreateMap<Address, AddressDTO>().ReverseMap();

    }
}
