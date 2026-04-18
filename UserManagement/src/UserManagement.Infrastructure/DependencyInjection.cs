using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Common.Interfaces;
using UserManagement.Infrastructure.Outbox;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Persistence.Repositories;
using UserManagement.Infrastructure.Persistence.Services;

namespace UserManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPersistence()
            .AddServices()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
        });

        services.AddHostedService<OutboxMessagesDispatcher>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasherService, PasswordHasherService>();

        return services;
    }
}