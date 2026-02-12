using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Movie;
using MovieDatabase.Application.Interfaces.Services;

namespace MovieDatabase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(IMovieService movieService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PageResult<MovieResponse>>> GetAll(
   [FromQuery] int pageNumber = 1,
   [FromQuery] int pageSize = 10,
   CancellationToken ct = default)
    {
        return Ok(await movieService.GetAllAsync(pageNumber, pageSize, ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieResponse>> GetById(int id, CancellationToken ct = default)
    {
        var result = await movieService.GetByIdAsync(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MovieResponse>> Create([FromBody] CreateMovieRequest request, CancellationToken ct = default)
    {
        var result = await movieService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieResponse>> UpdateAsync(
        int id,
        [FromBody] CreateMovieRequest request,
        CancellationToken ct = default)
    {
        try
        {
            var result = await movieService.UpdateAsync(id, request, ct);
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
        await movieService.DeleteAsync(id, ct);
        return NoContent();
    }
}
