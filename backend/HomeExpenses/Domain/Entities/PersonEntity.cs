using HomeExpenses.Domain.Abstractions;

namespace HomeExpenses.Domain.Entities;

public sealed class PersonEntity : IEntity
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public int Age { get; set; }

    public List<TransactionEntity> Transactions { get; set; } = [];
}
