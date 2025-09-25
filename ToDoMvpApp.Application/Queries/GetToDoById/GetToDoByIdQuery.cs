using MediatR;
using ToDoMvpApp.Application.Dto;

namespace ToDoMvpApp.Application.Queries.GetToDoById;

public record GetToDoByIdQuery(string Id) : IRequest<ToDoDto?>;
