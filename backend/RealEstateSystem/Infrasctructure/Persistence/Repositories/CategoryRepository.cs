using Microsoft.EntityFrameworkCore;
using RealEstateSystem.Application.Abstractions.Repositories;
using RealEstateSystem.Domain.Entities;

namespace RealEstateSystem.Infrasctructure.Persistence.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<CategoryEntity>> GetAll()
    {
        return await _context
            .Categories
            .AsNoTracking()
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
