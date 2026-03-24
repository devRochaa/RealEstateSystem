using Microsoft.EntityFrameworkCore;
using HomeExpenses.Application.Abstractions.Repositories;
using HomeExpenses.Domain.Entities;

namespace HomeExpenses.Infrasctructure.Persistence.Repositories;

public class PersonRepository(AppDbContext context) : IPersonRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<PersonEntity>> GetAll()
    {
        return await _context
            .Persons
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<PersonEntity>> GetAllWithTransactions()
    {
        return await _context
            .Persons
            .Include(p => p.Transactions)
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<PersonEntity?> GetById(Guid id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public async Task<PersonEntity> Create(PersonEntity entity)
    {
        _context.Persons.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Update(PersonEntity entity)
    {
        await _context.SaveChangesAsync();
    }

    public async Task Delete(PersonEntity entity)
    {
        _context.Persons.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
