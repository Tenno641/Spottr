using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SessionReservation.Infrastructure.Persistence;

namespace SessionReservation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPersistence();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<SessionReservationDbContext>(optios =>
        {
            optios.UseSqlServer(Environment.GetEnvironmentVariable("SessionReservationDatabaseConnectionString"));
        });

        return services;
    }
}