using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoMvpApp.Application.Dto;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Queries.GetToDoById;

public class GetToDoByIdQueryHandler(QueryDataBaseContext db) : IRequestHandler<GetToDoByIdQuery, ToDoDto?>
{
    private readonly QueryDataBaseContext _db = db;

    public async Task<ToDoDto?> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _db.ToDos
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Select(x => new ToDoDto(
                x.Id,
                x.Title,
                x.Description,
                x.DueDate,
                x.IsCompleted,
                x.IsImportant,
                x.Repeat,
                x.ReminderAt,
                x.InsertDateTime,
                x.UpdateDateTime
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return todo;
    }
}