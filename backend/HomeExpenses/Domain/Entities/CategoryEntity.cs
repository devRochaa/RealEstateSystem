using HomeExpenses.Domain.Abstractions;
using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Domain.Entities;

public class CategoryEntity : IEntity
{
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public CategoryPurposeEnum Purpose { get; set; }

    public List<TransactionEntity> Transactions { get; set; } = [];
}
