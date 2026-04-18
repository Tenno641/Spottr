using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Infrastructure.IntegrationEvents;
using SessionReservation.Infrastructure.Outbox;
using SessionReservation.Infrastructure.Persistence;
using SessionReservation.Infrastructure.Persistence.Repositories;

namespace SessionReservation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPersistence()
            .AddMasstransitConfiguration()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<IRoomsRepository, RoomsesRepository>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddHostedService<OutboxMessagesDispatcher>();
        
        services.AddDbContext<SessionReservationDbContext>(optios =>
        {
            optios.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
        });

        return services;
    }

    private static IServiceCollection AddMasstransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.AddConsumer<IntegrationEventsConsumer>();
            
            busConfig.AddEntityFrameworkOutbox<SessionReservationDbContext>(outboxConfig =>
            {
                outboxConfig.DuplicateDetectionWindow = TimeSpan.FromMinutes(5);
                outboxConfig.QueryMessageLimit = 10;
                outboxConfig.QueryDelay = TimeSpan.FromSeconds(30);
                outboxConfig.UseBusOutbox();
                outboxConfig.UsePostgres();
            });

            busConfig.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}