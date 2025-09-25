using MediatR;
using Microsoft.AspNetCore.Http;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Commands.ToDo.Create;

public class CreateTodoCommandHandler(CommandDataBaseContext db, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateTodoCommand, string>
{
    private readonly CommandDataBaseContext _db = db;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<string> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();

        var toDo = new Domain.Entities.ToDo
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            ReminderAt = request.ReminderAt,
            UserId = userId
        };

        _db.ToDos.Add(toDo);
        await _db.SaveChangesAsync(cancellationToken);

        return toDo.Id;
    }
}

