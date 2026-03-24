using HomeExpenses.Domain.Abstractions;
using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Domain.Entities;

public class TransactionEntity : IEntity
{
    public Guid Id { get; set; }

    public required string Description { get; set; }

    public decimal Amount { get; set; }

    public TransactionKindEnum Kind { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }

    public Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }
}
