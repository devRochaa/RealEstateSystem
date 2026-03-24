using HomeExpenses.Domain.Entities;

namespace HomeExpenses.Application.Abstractions.Repositories;

public interface ITransactionRepository
{
    Task<List<TransactionEntity>> GetAll();
    Task<TransactionEntity?> GetById(Guid id);
    Task<TransactionEntity> Create(TransactionEntity entity);
}
