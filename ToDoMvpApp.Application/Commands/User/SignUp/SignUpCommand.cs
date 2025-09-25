using MediatR;

namespace ToDoMvpApp.Application.Commands.User.SignUp;
public record SignUpCommand(string Username, string Password) : IRequest<string>;

