using Microsoft.AspNetCore.Mvc;
using HomeExpenses.Application.Abstractions.Services;
using HomeExpenses.Application.DTOs.Transaction;

namespace HomeExpenses.Controllers.TransactionController;

[ApiController]
[Route("transactions")]
public class TransactionController(ITransactionService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await service.GetAll();

        return Ok(result);
    }

    [HttpGet("{transactionId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid transactionId)
    {
        var result = await service.GetById(transactionId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto request)
    {
        var result = await service.Create(request);

        return CreatedAtAction(nameof(GetById), new { transactionId = result.Id }, result);
    }
}
