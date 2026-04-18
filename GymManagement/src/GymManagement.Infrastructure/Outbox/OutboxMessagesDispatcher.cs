using System.Data;
using System.Reflection;
using System.Text.Json;
using GymManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GymManagement.Infrastructure.Outbox;

public class OutboxMessagesDispatcher: BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<OutboxMessagesDispatcher> _logger;
    
    public OutboxMessagesDispatcher(IServiceScopeFactory serviceScopeFactory, ILogger<OutboxMessagesDispatcher> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            GymManagementDbContext dbContext = scope.ServiceProvider.GetRequiredService<GymManagementDbContext>();
            IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

            var outboxMessages = await dbContext.OutboxMessages
                .Where(m => !m.IsProcessed)
                .OrderBy(m => m.MessageId)
                .Take(50)
                .ToListAsync(stoppingToken);

            var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);
            
            try
            {
                string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                
                foreach (OutboxMessage outboxMessage in outboxMessages)
                {
                    if (assemblyName is null || !outboxMessage.Issuer.Equals(assemblyName, StringComparison.InvariantCultureIgnoreCase))
                        continue;
                    
                    if (await dbContext.OutboxMessages.CountAsync(m => m.MessageId == outboxMessage.MessageId, stoppingToken) > 1)
                        continue;

                    Type? type = Type.GetType(outboxMessage.Type);
                    
                    if (type is null)
                        continue;

                    var domainEvent = JsonSerializer.Deserialize(outboxMessage.Body, type);
                    
                    if (domainEvent is null)
                        continue;

                    outboxMessage.IsProcessed = true;

                    await publisher.Publish(domainEvent, stoppingToken);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
            await transaction.CommitAsync(stoppingToken);
            
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}