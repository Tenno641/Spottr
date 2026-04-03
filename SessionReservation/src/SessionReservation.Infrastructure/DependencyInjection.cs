using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Infrastructure.Persistence;
using SessionReservation.Infrastructure.Persistence.Repositories;

namespace SessionReservation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPersistence()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<IRoomRepository, RoomsRepository>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<SessionReservationDbContext>(optios =>
        {
            optios.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
        });

        return services;
    }
}