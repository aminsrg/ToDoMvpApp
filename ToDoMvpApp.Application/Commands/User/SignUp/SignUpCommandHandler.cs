using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ToDoMvpApp.Application.Extensions;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Commands.User.SignUp;

public class SignUpCommandHandler(CommandDataBaseContext db) : IRequestHandler<SignUpCommand, string>
{
    private readonly CommandDataBaseContext _db = db;

    public async Task<string> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (await _db.Users.AnyAsync(u => u.Username == request.Username, cancellationToken))
            throw new InvalidOperationException("Username already exists");

        var user = new ToDoMvpApp.Domain.Entities.User
        {
            Username = request.Username,
            PasswordHash = PasswordHasher.HashPassword(request.Password)
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}

