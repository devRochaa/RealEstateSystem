using Microsoft.EntityFrameworkCore;
using HomeExpenses.Application.Abstractions.Repositories;
using HomeExpenses.Domain.Entities;

namespace HomeExpenses.Infrasctructure.Persistence.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<CategoryEntity>> GetAll()
    {
        return await _context
            .Categories
            .AsNoTracking()
            .OrderBy(c => c.Description)
            .ToListAsync();
    }

    public async Task<List<CategoryEntity>> GetAllWithTransactions()
    {
        return await _context
            .Categories
            .Include(c => c.Transactions)
            .AsNoTracking()
            .OrderBy(c => c.Description)
            .ToListAsync();
    }

    public async Task<CategoryEntity?> GetById(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<CategoryEntity> Create(CategoryEntity entity)
    {
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
