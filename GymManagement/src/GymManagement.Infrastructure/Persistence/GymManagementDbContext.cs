using System.Reflection;
using System.Text.Json;
using GymManagement.Domain.AdminAggregate;
using GymManagement.Domain.Common;
using GymManagement.Domain.Common.Entities;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using GymManagement.Infrastructure.Outbox;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence;

public class GymManagementDbContext: DbContext
{
    private readonly IPublisher _publisher;
    private readonly IHttpContextAccessor _contextAccessor; 
    
    public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options, 
        IPublisher publisher, 
        IHttpContextAccessor contextAccessor) : base(options)
    {
        _contextAccessor = contextAccessor;
        _publisher = publisher;
    }
    
    public DbSet<Gym> Gyms { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        List<IDomainEvent> domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();

        List<OutboxMessage> outboxMessages = domainEvents.Select(@event =>
        {
            Type type = @event.GetType();
            return new OutboxMessage(type.AssemblyQualifiedName, JsonSerializer.Serialize(@event, type));
        }).ToList();
        
        OutboxMessages.AddRange(outboxMessages);

        // if (IsRequestStillBeingProcessed)
        // {
        //     AddDomainEventsToBeProcessed(domainEvents);
        //     return await base.SaveChangesAsync(cancellationToken);
        // }
        //
        // await PublishEvents(domainEvents);
        return base.SaveChangesAsync(cancellationToken);
    }

    private bool IsRequestStillBeingProcessed => _contextAccessor.HttpContext is not null;

    private async Task PublishEvents(List<IDomainEvent> events)
    {
        foreach (IDomainEvent @event in events)
            await _publisher.Publish(@event);
    }

    private void AddDomainEventsToBeProcessed(List<IDomainEvent> events)
    {
        Queue<IDomainEvent> domainEventsQueue = _contextAccessor.HttpContext.Items.TryGetValue("DomainEvents", out object? value)
            && value is Queue<IDomainEvent> existingDomainEvents
            ? existingDomainEvents
            : new Queue<IDomainEvent>();
        
        events.ForEach(domainEventsQueue.Enqueue);
        
        _contextAccessor.HttpContext.Items["DomainEvents"] = domainEventsQueue;
    }
}