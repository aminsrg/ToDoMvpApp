using MediatR;
using ToDoMvpApp.Domain.Enums;

namespace ToDoMvpApp.Application.Commands.ToDo.Update;

public record UpdateToDoCommand(
    string Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    bool IsCompleted,
    bool IsImportant,
    RepeatFrequency Repeat,
    DateTime? ReminderAt
) : IRequest;
