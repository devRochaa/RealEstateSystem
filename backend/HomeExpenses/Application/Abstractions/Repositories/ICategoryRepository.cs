using HomeExpenses.Domain.Entities;

namespace HomeExpenses.Application.Abstractions.Repositories;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAll();
    Task<List<CategoryEntity>> GetAllWithTransactions();
    Task<CategoryEntity?> GetById(Guid id);
    Task<CategoryEntity> Create(CategoryEntity entity);
}
     
