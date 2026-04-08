using GymManagement.Domain.Common;
using GymManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace GymManagement.Infrastructure.Middlewares;

public class EventualConsistencyMiddleware
{
    public const string DomainEventsKey = "DomainEvents";

    private readonly RequestDelegate _next;
    
    public EventualConsistencyMiddleware(RequestDelegate next)
    {
        _next = next;
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
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await _next(context);
    }
}