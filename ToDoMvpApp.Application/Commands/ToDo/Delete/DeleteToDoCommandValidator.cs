using FluentValidation;
using ToDoMvpApp.Application.Commands.ToDo.Delete;

namespace ToDoMvpApp.Application.Commands.Delete;

public class DeleteToDoCommandValidator : AbstractValidator<DeleteToDoCommand>
{
    public DeleteToDoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}



