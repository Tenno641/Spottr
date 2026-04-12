using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SessionReservation.Domain.Common;
using SessionReservation.Infrastructure.Persistence;

namespace SessionReservation.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<EventualConsistencyMiddleware> _logger;

    public EventualConsistencyMiddleware(RequestDelegate next, ILogger<EventualConsistencyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, IPublisher publisher, SessionReservationDbContext dbContext)
    {
        context.Response.OnCompleted(async () =>
        {
            IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                if (context.Items.TryGetValue("DomainEventsQueue", out object? value) && value is Queue<IDomainEvent> existingDomainEvents)
                {
                    while (existingDomainEvents.TryDequeue(out IDomainEvent? @event))
                    {
                        await publisher.Publish(@event);
                    }
                    await transaction.CommitAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred while processing a request");
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }
}