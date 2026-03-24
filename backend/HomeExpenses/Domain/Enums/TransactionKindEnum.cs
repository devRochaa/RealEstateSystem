using System.Text.Json.Serialization;

namespace HomeExpenses.Domain.Enums;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionKindEnum
{
    EXPENSE,
    INCOME
}
