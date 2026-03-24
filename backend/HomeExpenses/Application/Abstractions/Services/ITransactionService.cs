using HomeExpenses.Application.DTOs.Transaction;

namespace HomeExpenses.Application.Abstractions.Services;

public interface ITransactionService
{
    Task<List<TransactionDto>> GetAll();
    Task<TransactionDto> GetById(Guid id);
    Task<TransactionDto> Create(CreateTransactionDto dto);
}
