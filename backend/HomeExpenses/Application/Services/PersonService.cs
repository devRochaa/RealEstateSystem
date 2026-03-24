using HomeExpenses.Application.Abstractions.Repositories;
using HomeExpenses.Application.Abstractions.Services;
using HomeExpenses.Application.DTOs.Person;
using HomeExpenses.Application.DTOs.Reports;
using HomeExpenses.Domain.Entities;
using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.Services;

public class PersonService(IPersonRepository repository) : IPersonService
{
    private readonly IPersonRepository _repository = repository;

    public async Task<PersonDto> Create(CreatePersonDto dto)
    {
        dto.ValidateAndThrow();

        var person = new PersonEntity
        {
            Name = dto.Name,
            Age = dto.Age
        };

        await _repository.Create(person);

        return new PersonDto
        {
            Id = person.Id,
            Name = dto.Name,
            Age = dto.Age
        };
    }

    public async Task Remove(Guid id)
    {
        var person = await _repository.GetById(id)
            ?? throw new KeyNotFoundException("Pessoa nÃ£o encontrada.");

        await _repository.Delete(person);
    }

    public async Task<List<PersonDto>> GetAll()
    {
        var people = await _repository.GetAll();

        return people.Select(p => new PersonDto
        {
            Id = p.Id,
            Name = p.Name,
            Age = p.Age
        }).ToList();
    }

    public async Task<PersonDto> GetById(Guid id)
    {
        var person = await _repository.GetById(id)
            ?? throw new KeyNotFoundException("Pessoa nÃ£o encontrada.");

        return new PersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age
        };
    }

    public async Task<PersonDto?> Update(Guid id, UpdatePersonDto dto)
    {
        dto.ValidateAndThrow();

        var person = await _repository.GetById(id)
            ?? throw new KeyNotFoundException("Pessoa nÃ£o encontrada.");

        var nextName = dto.Name ?? person.Name;
        var nextAge = dto.Age ?? person.Age;

        if (person.Name != nextName || person.Age != nextAge)
        {
            person.Name = nextName;
            person.Age = nextAge;

            await _repository.Update(person);
        }

        return new PersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age
        };
    }

    public async Task<PersonTotalsDto> GetTotals()
    {
        var people = await _repository.GetAllWithTransactions();

        var items = people
            .OrderBy(p => p.Name)
            .Select(p =>
            {
                var totalIncome = p.Transactions
                    .Where(t => t.Kind == TransactionKindEnum.INCOME)
                    .Sum(t => t.Amount);

                var totalExpense = p.Transactions
                    .Where(t => t.Kind == TransactionKindEnum.EXPENSE)
                    .Sum(t => t.Amount);

                return new PersonBalanceDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    TotalIncome = totalIncome,
                    TotalExpense = totalExpense,
                    Balance = totalIncome - totalExpense
                };
            })
            .ToList();

        return new PersonTotalsDto
        {
            People = items,
            Totals = new TotalsSummaryDto
            {
                TotalIncome = items.Sum(x => x.TotalIncome),
                TotalExpense = items.Sum(x => x.TotalExpense),
                Balance = items.Sum(x => x.Balance)
            }
        };
    }
}
