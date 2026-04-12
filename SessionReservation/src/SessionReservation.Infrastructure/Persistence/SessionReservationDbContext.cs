using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Infrastructure.Persistence;

public class SessionReservationDbContext: DbContext
{
    private readonly IHttpContextAccessor _httpHttpContextAccessor;
    private readonly IPublisher _publisher;
    
    public SessionReservationDbContext(
        DbContextOptions<SessionReservationDbContext> options, 
        IHttpContextAccessor httpHttpContextAccessor, 
        IPublisher publisher): base(options)
    {
        _httpHttpContextAccessor = httpHttpContextAccessor;
        _publisher = publisher;
    }
    
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Trainer> Trainers{ get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        List<IDomainEvent> domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.PopDomainEvents())
            .ToList();

        if (IsRequestBeingProcessed)
        {
            AddDomainEventsToProcessingQueue(domainEvents);
            return await base.SaveChangesAsync(cancellationToken);
        }

        await PublishEvents(domainEvents);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private bool IsRequestBeingProcessed => _httpHttpContextAccessor.HttpContext is not null;

    private async Task PublishEvents(List<IDomainEvent> domainEvents)
    {
        foreach (IDomainEvent @event in domainEvents)
            await _publisher.Publish(@event);
    }

    private void AddDomainEventsToProcessingQueue(List<IDomainEvent> events)
    {
        const string eventsQueue = "DomainEventsQueue";

        Queue<IDomainEvent> domainEventsQueue = _httpHttpContextAccessor.HttpContext.Items.TryGetValue(eventsQueue, out object? value)
            && value is Queue<IDomainEvent> existingDomainEventsQueue
            ? existingDomainEventsQueue
            : new Queue<IDomainEvent>();
        
        events.ForEach(domainEventsQueue.Enqueue);
        
        _httpHttpContextAccessor.HttpContext.Items.Add(eventsQueue, domainEventsQueue);
    }
}