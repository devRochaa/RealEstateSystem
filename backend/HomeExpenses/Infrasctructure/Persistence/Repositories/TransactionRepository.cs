using Microsoft.EntityFrameworkCore;
using HomeExpenses.Application.Abstractions.Repositories;
using HomeExpenses.Domain.Entities;

namespace HomeExpenses.Infrasctructure.Persistence.Repositories;

public sealed class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<TransactionEntity>> GetAll()
    {
        return await _context
            .Transactions
            .Include(t => t.Category)
            .Include(t => t.Person)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TransactionEntity?> GetById(Guid id)
    {
        return await _context
            .Transactions
            .Include(t => t.Category)
            .Include(t => t.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TransactionEntity> Create(TransactionEntity entity)
    {
        _context.Transactions.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
