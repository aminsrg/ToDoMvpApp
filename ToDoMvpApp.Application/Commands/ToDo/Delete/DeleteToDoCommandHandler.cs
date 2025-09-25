using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Commands.ToDo.Delete;

public class DeleteToDoCommandHandler(CommandDataBaseContext db) : IRequestHandler<DeleteToDoCommand>
{
    private readonly CommandDataBaseContext _db = db;

    public async Task Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _db.ToDos
            .FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted, cancellationToken);

        if (todo is null) throw new KeyNotFoundException("Todo not found");

        todo.IsDeleted = true;
        todo.UpdateDateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
    }
}



