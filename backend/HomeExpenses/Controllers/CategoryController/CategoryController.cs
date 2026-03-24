using Microsoft.AspNetCore.Mvc;
using HomeExpenses.Application.Abstractions.Services;
using HomeExpenses.Application.DTOs.Category;

namespace HomeExpenses.Controllers.CategoryController;

[ApiController]
[Route("categories")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await service.GetAll();

        return Ok(result);
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid categoryId)
    {
        var result = await service.GetById(categoryId);

        return Ok(result);
    }

    [HttpGet("totals")]
    public async Task<IActionResult> GetTotals()
    {
        var result = await service.GetTotals();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto request)
    {
        var result = await service.Create(request);

        return CreatedAtAction(nameof(GetById), new { categoryId = result.Id }, result);
    }
}
