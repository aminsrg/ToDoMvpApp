using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoMvpApp.Domain.Entities;
using ToDoMvpApp.Domain.Enums;

namespace ToDoMvpApp.Infrastructure.Persistence;
public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
               .IsRequired();

        builder.Property(t => t.Title)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(t => t.Description)
               .HasMaxLength(1000);

        builder.Property(t => t.DueDate);

        builder.Property(t => t.IsCompleted)
               .HasDefaultValue(false);

        builder.Property(t => t.IsDeleted)
               .HasDefaultValue(false);

        builder.Property(t => t.IsImportant)
               .HasDefaultValue(false);

        builder.Property(t => t.Repeat)
               .HasConversion<string>() // map enum as text
               .HasDefaultValue(RepeatFrequency.None);

        builder.Property(t => t.ReminderAt);

        builder.Property(t => t.InsertDateTime)
               .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
               .IsRequired();

        builder.Property(t => t.UpdateDateTime);

        builder.HasOne(t => t.User)
               .WithMany(u => u.Todos)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
               .IsRequired();

        builder.Property(u => u.Username)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(u => u.Username).IsUnique();

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.Property(u => u.InsertDateTime)
               .HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'")
               .IsRequired();
    }
}
