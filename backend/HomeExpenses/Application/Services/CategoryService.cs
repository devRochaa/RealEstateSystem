using HomeExpenses.Application.Abstractions.Repositories;
using HomeExpenses.Application.Abstractions.Services;
using HomeExpenses.Application.DTOs.Category;
using HomeExpenses.Application.DTOs.Reports;
using HomeExpenses.Domain.Entities;
using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.Services;

public sealed class CategoryService(ICategoryRepository repository) : ICategoryService
{
    private readonly ICategoryRepository _repository = repository;

    public async Task<List<CategoryDto>> GetAll()
    {
        var query = await _repository.GetAll();

        return query.Select(e => new CategoryDto
        {
            Id = e.Id,
            Description = e.Description,
            Purpose = e.Purpose
        }).ToList();
    }

    public async Task<CategoryDto> GetById(Guid id)
    {
        var entity = await _repository.GetById(id)
            ?? throw new KeyNotFoundException("Categoria nÃ£o encontrada.");

        return new CategoryDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Purpose = entity.Purpose
        };
    }

    public async Task<CategoryDto> Create(CreateCategoryDto dto)
    {
        dto.ValidateAndThrow();

        var entity = new CategoryEntity
        {
            Description = dto.Description,
            Purpose = dto.Purpose
        };

        await _repository.Create(entity);

        return new CategoryDto
        {
            Id = entity.Id,
            Description = dto.Description,
            Purpose = dto.Purpose
        };
    }

    public async Task<CategoryTotalsDto> GetTotals()
    {
        var categories = await _repository.GetAllWithTransactions();

        var items = categories
            .OrderBy(c => c.Description)
            .Select(c =>
            {
                var totalIncome = c.Transactions
                    .Where(t => t.Kind == TransactionKindEnum.INCOME)
                    .Sum(t => t.Amount);

                var totalExpense = c.Transactions
                    .Where(t => t.Kind == TransactionKindEnum.EXPENSE)
                    .Sum(t => t.Amount);

                return new CategoryBalanceDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    Purpose = c.Purpose,
                    TotalIncome = totalIncome,
                    TotalExpense = totalExpense,
                    Balance = totalIncome - totalExpense
                };
            })
            .ToList();

        return new CategoryTotalsDto
        {
            Categories = items,
            Totals = new TotalsSummaryDto
            {
                TotalIncome = items.Sum(x => x.TotalIncome),
                TotalExpense = items.Sum(x => x.TotalExpense),
                Balance = items.Sum(x => x.Balance)
            }
        };
    }
}
