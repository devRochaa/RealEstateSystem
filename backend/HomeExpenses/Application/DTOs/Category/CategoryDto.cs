using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.DTOs.Category;

public sealed class CategoryDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public CategoryPurposeEnum Purpose { get; set; }
}
