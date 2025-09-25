using MediatR;

namespace ToDoMvpApp.Application.Commands.ToDo.Create;
public record CreateTodoCommand(string Title, string? Description, DateTime? DueDate, DateTime? ReminderAt) : IRequest<string>;
