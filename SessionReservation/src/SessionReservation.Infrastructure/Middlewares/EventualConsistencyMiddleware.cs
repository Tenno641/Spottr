using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using SessionReservation.Domain.Common;
using SessionReservation.Infrastructure.Persistence;

namespace SessionReservation.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware
{
    private readonly RequestDelegate _next;

    public EventualConsistencyMiddleware(RequestDelegate next, IPublisher publisher)
    {
        _next = next;
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
                        publisher.Publish(@event);
                    }
                    await transaction.CommitAsync();
                }
            }
            catch (Exception e)
            {
                // TODO: Use Polly Library
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }
}