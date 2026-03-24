using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.DTOs.Reports;

public sealed class CategoryBalanceDto
{
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public CategoryPurposeEnum Purpose { get; set; }

    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }
}
