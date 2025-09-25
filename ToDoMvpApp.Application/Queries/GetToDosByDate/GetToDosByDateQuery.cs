using MediatR;
using ToDoMvpApp.Application.Dto;

namespace ToDoMvpApp.Application.Queries.GetToDosByDate;

public record GetToDosByDateQuery(DateTime Date) : IRequest<IEnumerable<ToDoDto>>;
