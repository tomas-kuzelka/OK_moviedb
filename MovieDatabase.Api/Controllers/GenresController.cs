using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Genre;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Application.Interfaces.Services;
using MovieDatabase.Application.Services;
using MovieDatabase.Domain.Entities;

namespace MovieDatabase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{


    [HttpGet]
    public async Task<ActionResult<PageResult<GenreResponse>>> GetAll(
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10,
       CancellationToken ct = default)
    {
        return Ok(await genreService.GetAllAsync(pageNumber, pageSize, ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreResponse>> GetById(int id, CancellationToken ct = default)
    {
        var result = await genreService.GetByIdAsync(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponse>> Create([FromBody] CreateGenreRequest request, CancellationToken ct = default)
    {
        var result = await genreService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GenreResponse>> UpdateAsync(
        int id,
        [FromBody] CreateGenreRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var result = await genreService.UpdateAsync(id, request, ct);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        await genreService.DeleteAsync(id, ct);
        return NoContent();
    }

}
