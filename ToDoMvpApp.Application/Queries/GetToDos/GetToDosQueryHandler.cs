using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ToDoMvpApp.Application.Dto;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Queries.GetToDos;

public class GetToDosQueryHandler(QueryDataBaseContext db, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetToDosQuery, IEnumerable<ToDoDto>>
{
    private readonly QueryDataBaseContext _db = db;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<IEnumerable<ToDoDto>> Handle(GetToDosQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();

        var q = _db.ToDos.Where(x => !x.IsDeleted && x.UserId == userId);

        if (request.IsCompleted.HasValue)
            q = q.Where(x => x.IsCompleted == request.IsCompleted.Value);

        if (request.IsImportant.HasValue)
            q = q.Where(x => x.IsImportant == request.IsImportant.Value);

        var ToDos = await q
            .OrderByDescending(x => x.InsertDateTime)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
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
            .ToListAsync(cancellationToken);

        return ToDos;
    }
}
