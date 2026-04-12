using GymManagement.Domain.Common;
using GymManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace GymManagement.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware
{
    public const string DomainEventsKey = "DomainEvents";

    private readonly RequestDelegate _next;
    private readonly ILogger<EventualConsistencyMiddleware> _logger;
    
    public EventualConsistencyMiddleware(RequestDelegate next, ILogger<EventualConsistencyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, GymManagementDbContext dbContext)
    {
        IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out object? value) && value is Queue<IDomainEvent> existingDomainEvents)
                {
                    while (existingDomainEvents.TryDequeue(out IDomainEvent? @event))
                        await publisher.Publish(@event);

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