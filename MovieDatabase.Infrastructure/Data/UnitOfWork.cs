using MovieDatabase.Application.Interfaces;
using MovieDatabase.Application.Interfaces.Repositories;
using MovieDatabase.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{

    private readonly MoviesDbContext _context;

    // lazy loading (odložená inicializace) repozitářů
    private IPersonRepository? _personRepository;
    private IMovieRepository? _movieRepository;
    private IGenreRepository? _genreRepository;

    public UnitOfWork(MoviesDbContext context)
    {
        _context = context;
    }

    // ??= (null-coalescing assignment): pokud se někdo zeptá na MovieRepository a _movieRepository bude null, tak se vytvoří nová instanace
    public IMovieRepository MoviRepository => _movieRepository ??= new MovieRepository(_context);
    public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(_context);
    public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);

    public async Task Begin(CancellationToken ct = default)
    {
        await _context.Database.BeginTransactionAsync(ct);
    }
    public async Task<int> CommitAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public void RollBack(CancellationToken ct = default)
    {
        _context.ChangeTracker.Clear();
    }
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this); // říká garbage collectoru, že objekt byl už manuálně uklizen
    }
}
