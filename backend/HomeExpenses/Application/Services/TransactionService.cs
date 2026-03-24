using HomeExpenses.Application.Abstractions.Repositories;
using HomeExpenses.Application.Abstractions.Services;
using HomeExpenses.Application.DTOs.Transaction;
using HomeExpenses.Domain.Entities;
using HomeExpenses.Domain.Enums;

namespace HomeExpenses.Application.Services;

public sealed class TransactionService(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository,
    IPersonRepository personRepository) : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IPersonRepository _personRepository = personRepository;

    public async Task<List<TransactionDto>> GetAll()
    {
        var transactions = await _transactionRepository.GetAll();

        return transactions
            .OrderByDescending(t => t.Id)
            .Select(MapToDto)
            .ToList();
    }

    public async Task<TransactionDto> GetById(Guid id)
    {
        var transaction = await _transactionRepository.GetById(id)
            ?? throw new KeyNotFoundException("TransaÃ§Ã£o nÃ£o encontrada.");

        return MapToDto(transaction);
    }

    public async Task<TransactionDto> Create(CreateTransactionDto dto)
    {
        dto.ValidateAndThrow();

        var person = await _personRepository.GetById(dto.PersonId)
            ?? throw new KeyNotFoundException("Pessoa nÃ£o encontrada.");

        var category = await _categoryRepository.GetById(dto.CategoryId)
            ?? throw new KeyNotFoundException("Categoria nÃ£o encontrada.");

        if (person.Age < 18 && dto.Kind != TransactionKindEnum.EXPENSE)
            throw new InvalidOperationException("Menores de idade podem registrar apenas despesas.");

        if (!CategorySupportsTransaction(category.Purpose, dto.Kind))
            throw new InvalidOperationException("A categoria informada nÃ£o aceita o tipo de transaÃ§Ã£o selecionado.");

        var entity = new TransactionEntity
        {
            Description = dto.Description,
            Amount = dto.Amount,
            Kind = dto.Kind,
            CategoryId = dto.CategoryId,
            PersonId = dto.PersonId
        };

        await _transactionRepository.Create(entity);

        entity.Category = category;
        entity.Person = person;

        return MapToDto(entity);
    }

    private static bool CategorySupportsTransaction(CategoryPurposeEnum purpose, TransactionKindEnum kind)
    {
        return purpose == CategoryPurposeEnum.BOTH ||
               (purpose == CategoryPurposeEnum.EXPENSE && kind == TransactionKindEnum.EXPENSE) ||
               (purpose == CategoryPurposeEnum.INCOME && kind == TransactionKindEnum.INCOME);
    }

    private static TransactionDto MapToDto(TransactionEntity entity)
    {
        return new TransactionDto
        {
            Id = entity.Id,
            Description = entity.Description,
            Amount = entity.Amount,
            Kind = entity.Kind,
            CategoryId = entity.CategoryId,
            CategoryDescription = entity.Category?.Description ?? string.Empty,
            PersonId = entity.PersonId,
            PersonName = entity.Person?.Name ?? string.Empty
        };
    }
}
