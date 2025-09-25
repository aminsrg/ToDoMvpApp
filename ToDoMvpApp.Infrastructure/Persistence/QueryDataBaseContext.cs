using Microsoft.EntityFrameworkCore;
using ToDoMvpApp.Domain.Entities;
using ToDoMvpApp.Domain.Enums;

namespace ToDoMvpApp.Infrastructure.Persistence;
public class QueryDataBaseContext(DbContextOptions<QueryDataBaseContext> options) : DbContext(options)
{
    public DbSet<ToDo> ToDos { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<RepeatFrequency>();
        modelBuilder.ApplyConfiguration(new ToDoConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public override DbSet<TEntity> Set<TEntity>() =>
         (DbSet<TEntity>)base.Set<TEntity>().AsNoTracking();
}
