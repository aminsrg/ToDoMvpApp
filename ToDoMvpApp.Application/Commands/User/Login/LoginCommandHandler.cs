using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ToDoMvpApp.Application.Extensions;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Application.Commands.User.Login;

public class LoginCommandHandler(QueryDataBaseContext db) : IRequestHandler<LoginCommand, string?>
{
    private readonly QueryDataBaseContext _db = db;

    public async Task<string?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user is null) return null;

        return PasswordHasher.VerifyPassword(request.Password, user.PasswordHash)
            ? user.Id
            : null;
    }
}

