using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.DTOs.Transaction;

public sealed class TransactionDto
{
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public TransactionKindEnum Kind { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryDescription { get; set; } = string.Empty;

    public Guid PersonId { get; set; }

    public string PersonName { get; set; } = string.Empty;
}
