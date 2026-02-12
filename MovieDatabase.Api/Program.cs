using Microsoft.EntityFrameworkCore;
using MovieDatabase.Application.Interfaces;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Application.Interfaces.Services;
using MovieDatabase.Application.Mappings;
using MovieDatabase.Application.Services;
using MovieDatabase.Infrastructure.Data;
using MovieDatabase.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MoviesDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<MappingProfile>();
});

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
