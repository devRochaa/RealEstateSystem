namespace HomeExpenses.Application.DTOs.Reports;

public sealed class CategoryTotalsDto
{
    public List<CategoryBalanceDto> Categories { get; set; } = [];

    public TotalsSummaryDto Totals { get; set; } = new();
}
