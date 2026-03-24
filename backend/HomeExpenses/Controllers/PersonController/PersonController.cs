using Microsoft.AspNetCore.Mvc;
using HomeExpenses.Application.Abstractions.Services;
using HomeExpenses.Application.DTOs.Person;

namespace HomeExpenses.Controllers.PersonController;

[ApiController]
[Route("persons")]
public class PersonController(IPersonService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await service.GetAll();

        return Ok(result);
    }

    [HttpGet("{personId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid personId)
    {
        var result = await service.GetById(personId);

        return Ok(result);
    }

    [HttpGet("totals")]
    public async Task<IActionResult> GetTotals()
    {
        var result = await service.GetTotals();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonDto request)
    {
        var result = await service.Create(request);

        return CreatedAtAction(nameof(GetById), new { personId = result.Id }, result);
    }

    [HttpPut("{personId}")]
    public async Task<IActionResult> Update([FromRoute] Guid personId, [FromBody] UpdatePersonDto request)
    {
        var result = await service.Update(personId, request);

        return Ok(result);
    }

    [HttpDelete("{personId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid personId)
    {
        await service.Remove(personId);

        return NoContent();
    }
}
