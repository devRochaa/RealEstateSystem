using Microsoft.EntityFrameworkCore;
using HomeExpenses.Domain.Entities;

namespace HomeExpenses.Infrasctructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PersonEntity>(entity =>
        {
            entity.ToTable(nameof(PersonEntity).Replace("Entity", string.Empty));

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasDefaultValueSql("NEWSEQUENTIALID()");

            entity.Property(p => p.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.HasMany(p => p.Transactions)
                  .WithOne(t => t.Person)
                  .HasForeignKey(t => t.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.ToTable(nameof(CategoryEntity).Replace("Entity", string.Empty));

            entity.HasKey(c => c.Id);

            entity.Property(p => p.Id)
                  .HasDefaultValueSql("NEWSEQUENTIALID()");

            entity.Property(c => c.Description)
                  .IsRequired()
                  .HasMaxLength(400);

            entity.Property(c => c.Purpose)
                  .HasConversion<string>()
                  .IsRequired();

            entity.HasMany(c => c.Transactions)
                  .WithOne(t => t.Category)
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.ToTable(nameof(TransactionEntity).Replace("Entity", string.Empty));

            entity.HasKey(t => t.Id);

            entity.Property(p => p.Id)
                  .HasDefaultValueSql("NEWSEQUENTIALID()");

            entity.Property(t => t.Description)
                  .IsRequired()
                  .HasMaxLength(400);

            entity.Property(t => t.Amount)
                  .HasPrecision(18, 2)
                  .IsRequired();

            entity.Property(t => t.Kind)
                  .HasConversion<string>()
                  .IsRequired();

            entity.HasOne(t => t.Person)
                  .WithMany(p => p.Transactions)
                  .HasForeignKey(t => t.PersonId);
        });
    }
}
