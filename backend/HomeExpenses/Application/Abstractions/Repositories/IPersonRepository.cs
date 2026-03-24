using HomeExpenses.Domain.Entities;

namespace HomeExpenses.Application.Abstractions.Repositories;

public interface IPersonRepository
{
    Task<List<PersonEntity>> GetAll();
    Task<List<PersonEntity>> GetAllWithTransactions();
    Task<PersonEntity?> GetById(Guid id);
    Task<PersonEntity> Create(PersonEntity entity);
    Task Update(PersonEntity entity);
    Task Delete(PersonEntity entity);
}
