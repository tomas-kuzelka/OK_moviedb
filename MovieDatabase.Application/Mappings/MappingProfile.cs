using AutoMapper;
using MovieDatabase.Application.DTOs.Genre;
using MovieDatabase.Application.DTOs.Movie;
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

        // Address Mapping
        CreateMap<Address, AddressDTO>().ReverseMap();

        // Movie Mappings
        CreateMap<Movie, MovieSummaryDTO>().ReverseMap();
        CreateMap<CreateMovieRequest, Movie>()
            .ForMember(dest => dest.Director, opt => opt.Ignore())
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore());

        CreateMap<Movie, MovieResponse>()
            .ForMember(dest => dest.DirectorName,
            opt => opt.MapFrom(src => src.Director != null ? src.Director.Name : string.Empty))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name)));

        // Genre Mappings
        CreateMap<Genre, GenreResponse>();
        CreateMap<CreateGenreRequest, Genre>()
            .ForMember(dest => dest.Movies, opt => opt.Ignore());
    }
}
