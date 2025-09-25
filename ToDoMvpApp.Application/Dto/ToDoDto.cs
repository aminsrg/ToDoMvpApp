using ToDoMvpApp.Domain.Enums;

namespace ToDoMvpApp.Application.Dto;

public record ToDoDto(
    string Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsCompleted,
    bool IsImportant,
    RepeatFrequency Repeat,
    DateTime? ReminderAt,
    DateTime InsertDateTime,
    DateTime UpdateDateTime
);
