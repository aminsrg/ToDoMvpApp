using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Commands.ToDo.Update;

public class UpdateToDoCommandHandler(CommandDataBaseContext db) : IRequestHandler<UpdateToDoCommand>
{
    private readonly CommandDataBaseContext _db = db;

    public async Task Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _db.ToDos
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

        if (todo is null) throw new KeyNotFoundException("Todo not found");

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.DueDate = request.DueDate;
        todo.IsCompleted = request.IsCompleted;
        todo.IsImportant = request.IsImportant;
        todo.Repeat = request.Repeat;
        todo.ReminderAt = request.ReminderAt;
        todo.UpdateDateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
    }
}
