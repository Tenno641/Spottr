using Microsoft.Extensions.DependencyInjection;

namespace SessionReservation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            configure.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });

        return services;
    }
}