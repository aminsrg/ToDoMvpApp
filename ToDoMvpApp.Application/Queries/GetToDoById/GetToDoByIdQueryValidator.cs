using FluentValidation;

namespace ToDoMvpApp.Application.Queries.GetToDoById;

public class GetToDoByIdQueryValidator : AbstractValidator<GetToDoByIdQuery>
{
    public GetToDoByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
