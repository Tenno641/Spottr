using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SessionReservation.Infrastructure.Persistence;

namespace SessionReservation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddDatabase();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<SessionReservationDbContext>(optios =>
        {
            optios.UseSqlServer(Environment.GetEnvironmentVariable(
                Environment.GetEnvironmentVariable("SessionReservationDatabaseConnectionString") ??
                throw new ArgumentException()));
        });

        return services;
    }
}