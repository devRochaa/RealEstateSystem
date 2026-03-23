using RealEstateSystem.Application.DTOs.Category;

namespace RealEstateSystem.Application.Abstractions.Services;

public interface ICategoryService
{
    public Task<List<CategoryDto>> GetAll();
    public Task<CategoryDto> GetById(Guid id);
    public Task<CategoryDto> Create(CreateCategoryDto dto);
}
