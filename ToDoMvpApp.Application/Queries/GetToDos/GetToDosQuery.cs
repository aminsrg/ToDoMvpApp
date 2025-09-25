using MediatR;
using ToDoMvpApp.Application.Dto;

namespace ToDoMvpApp.Application.Queries.GetToDos;

public record GetToDosQuery(
    int Page = 1,
    int PageSize = 20,
    bool? IsCompleted = null,
    bool? IsImportant = null
) : IRequest<IEnumerable<ToDoDto>>;
