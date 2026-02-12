using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Application.DTOs.Person;
using MovieDatabase.Application.Interfaces.Services;

namespace MovieDatabase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController(IPersonService personService) : ControllerBase
{


    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> GetAll(CancellationToken ct = default)
    {
        return Ok(await personService.GetAllAsync(ct));
    }

    [HttpPost]
    public async Task<ActionResult<PersonResponse>> Create([FromBody] CreatePersonRequest personRequest, CancellationToken ct = default)
    {
        var result = await personService.CreateAsync(personRequest, ct);
        return Ok(result);
    }
}
