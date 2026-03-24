namespace HomeExpenses.Application.DTOs.Reports;

public sealed class TotalsSummaryDto
{
    public decimal TotalIncome { get; set; }

    public decimal TotalExpense { get; set; }

    public decimal Balance { get; set; }
}
