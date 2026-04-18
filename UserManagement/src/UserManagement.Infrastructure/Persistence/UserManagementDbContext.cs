using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Common;
using UserManagement.Domain.UserAggregate;
using UserManagement.Infrastructure.Outbox;

namespace UserManagement.Infrastructure.Persistence;

public class UserManagementDbContext: DbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options): base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        List<IDomainEvent> domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => entity.PopDomainEvents())
            .ToList();
        
        List<OutboxMessage> outboxMessages = domainEvents.Select(@event =>
        {
            Type type = @event.GetType();
            return new OutboxMessage(type.AssemblyQualifiedName, JsonSerializer.Serialize(@event, type));
        }).ToList();
        
        OutboxMessages.AddRange(outboxMessages);
        
        return base.SaveChangesAsync(cancellationToken);
    }

}