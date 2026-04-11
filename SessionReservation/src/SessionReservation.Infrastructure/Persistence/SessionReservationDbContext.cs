using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.Gyms;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Infrastructure.Persistence;

public class SessionReservationDbContext: DbContext
{
    private readonly IHttpContextAccessor _httpHttpContextAccessor;
    
    public SessionReservationDbContext(DbContextOptions<SessionReservationDbContext> options, IHttpContextAccessor httpHttpContextAccessor): base(options)
    {
        _httpHttpContextAccessor = httpHttpContextAccessor;
    }
    
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Trainer> Trainers{ get; set; }
    public DbSet<Gym> Gyms { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        List<IDomainEvent> domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.PopDomainEvents())
            .ToList();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddDomainEventsToProcessingQueue(List<IDomainEvent> events)
    {
        const string eventsQueue = "DomainEventsQueue";

        Queue<IDomainEvent> domainEventsQueue = _httpHttpContextAccessor.HttpContext.Items.TryGetValue(eventsQueue, out object? value)
            && value is Queue<IDomainEvent> existingDomainEventsQueue
            ? existingDomainEventsQueue
            : new Queue<IDomainEvent>();
        
        events.ForEach(@event => domainEventsQueue.Enqueue(@event));
        
        _httpHttpContextAccessor.HttpContext.Items.Add(eventsQueue, domainEventsQueue);
    }
}