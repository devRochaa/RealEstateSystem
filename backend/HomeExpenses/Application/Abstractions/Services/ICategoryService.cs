using HomeExpenses.Application.DTOs.Category;
using HomeExpenses.Application.DTOs.Reports;

namespace HomeExpenses.Application.Abstractions.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAll();
    Task<CategoryDto> GetById(Guid id);
    Task<CategoryDto> Create(CreateCategoryDto dto);
    Task<CategoryTotalsDto> GetTotals();
}
