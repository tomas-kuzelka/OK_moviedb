using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Application.DTOs.Common;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Application.Interfaces.Services;
using MovieDatabase.Domain.Entities;

namespace MovieDatabase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController(IPersonService personService) : ControllerBase
{


    [HttpGet]
    public async Task<ActionResult<PageResult<PersonResponse>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        return Ok(await personService.GetAllAsync(pageNumber, pageSize, ct));
    }

    [HttpGet("actors")]
    public async Task<ActionResult<PageResult<PersonResponse>>> GetAllActorsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10
        , CancellationToken ct = default)
    {
        var result = await personService.GetAllPersonsAsync(PersonRole.Actor, pageNumber, pageSize, ct);
        return Ok(result);
    }

    [HttpGet("directors")]
    public async Task<ActionResult<PageResult<PersonResponse>>> GetAllDirectorsAsync(
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10
       , CancellationToken ct = default)
    {
        var result = await personService.GetAllPersonsAsync(PersonRole.Director, pageNumber, pageSize, ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponse>> Create([FromBody] CreatePersonRequest personRequest, CancellationToken ct = default)
    {
        var result = await personService.CreateAsync(personRequest, ct);
        return Ok(result);
    }
}
