using GymManagement.Application.Common.Interface;
using GymManagement.Infrastructure.Persistence;
using GymManagement.Infrastructure.Persistence.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddPersistence()
            .AddMassTransitConfigurations();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        
        services.AddDbContext<GymManagementDbContext>(optios =>
        {
            optios.UseSqlServer(Environment.GetEnvironmentVariable("DatabaseConnectionString"));
        });
        
        return services;
    }

    private static IServiceCollection AddMassTransitConfigurations(this IServiceCollection services)
    {
        services.AddMassTransit(busConfig =>
        {
            busConfig.AddEntityFrameworkOutbox<GymManagementDbContext>(outboxConfig =>
            {
                outboxConfig.DuplicateDetectionWindow = TimeSpan.FromMinutes(5);
                outboxConfig.QueryMessageLimit = 10;
                outboxConfig.QueryDelay = TimeSpan.FromSeconds(30);
                outboxConfig.UseBusOutbox();
                outboxConfig.UseSqlServer();
            });
            
            busConfig.UsingRabbitMq((context, cfg) =>
            {
                
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}