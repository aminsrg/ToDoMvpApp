using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoMvpApp.Infrastructure.Persistence;

namespace ToDoMvpApp.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("TODO_DB_CONNECTION")
                   ?? throw new InvalidOperationException("Environment variable TODO_DB_CONNECTION is not set.");

        services.AddDbContext<CommandDataBaseContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddDbContext<QueryDataBaseContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
