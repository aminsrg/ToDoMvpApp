using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ToDoMvpApp.Application.Dto;
using ToDoMvpApp.Domain.Enums;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Queries.GetToDosByDate;

public class GetToDosByDateQueryHandler(QueryDataBaseContext db, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetToDosByDateQuery, IEnumerable<ToDoDto>>
{
    private readonly QueryDataBaseContext _db = db;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<IEnumerable<ToDoDto>> Handle(GetToDosByDateQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();

        var targetDate = request.Date.Date;

        var ToDos = await _db.ToDos
            .Where(x => !x.IsDeleted && x.UserId == userId && x.DueDate != null && (
                (x.Repeat == RepeatFrequency.None && x.DueDate.Value.Date == targetDate)
                || (x.Repeat == RepeatFrequency.Daily)
                || (x.Repeat == RepeatFrequency.Weekly && x.DueDate.Value.DayOfWeek == targetDate.DayOfWeek)
                || (x.Repeat == RepeatFrequency.Monthly && x.DueDate.Value.Day == targetDate.Day)
                || (x.Repeat == RepeatFrequency.Yearly &&
                    x.DueDate.Value.Day == targetDate.Day &&
                    x.DueDate.Value.Month == targetDate.Month)
            ))
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