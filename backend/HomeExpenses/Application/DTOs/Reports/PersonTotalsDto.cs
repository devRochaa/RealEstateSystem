namespace HomeExpenses.Application.DTOs.Reports;

public sealed class PersonTotalsDto
{
    public List<PersonBalanceDto> People { get; set; } = [];

    public TotalsSummaryDto Totals { get; set; } = new();
}
