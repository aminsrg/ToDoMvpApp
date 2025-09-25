using MediatR;

namespace ToDoMvpApp.Application.Commands.User.Login;

public record LoginCommand(string Username, string Password) : IRequest<string?>;

