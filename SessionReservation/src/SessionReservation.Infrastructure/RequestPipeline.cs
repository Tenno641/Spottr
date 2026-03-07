using Microsoft.AspNetCore.Builder;
using SessionReservation.Infrastructure.Middlewares;

namespace SessionReservation.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder UseEventualConsistencyMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();

        return app;
    }
}