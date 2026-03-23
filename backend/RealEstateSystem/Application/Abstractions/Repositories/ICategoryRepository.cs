using RealEstateSystem.Domain.Entities;

namespace RealEstateSystem.Application.Abstractions.Repositories;

public interface ICategoryRepository
{
    Task<List<CategoryEntity>> GetAll();
    Task<CategoryEntity> GetById(Guid id);
    Task<CategoryEntity> Create(CategoryEntity entity);
}
     