using MovieDatabase.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Application.Interfaces;


public interface IUnitOfWork : IDisposable
{
    IMovieRepository MovieRepository { get; }
    IPersonRepository PersonRepository { get; }
    IGenreRepository GenreRepository { get; }


    Task<int> CommitAsync(CancellationToken ct = default);

    Task Begin(CancellationToken ct = default);

    void RollBack(CancellationToken ct = default);



}
