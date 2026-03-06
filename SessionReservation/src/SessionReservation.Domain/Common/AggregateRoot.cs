namespace SessionReservation.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected readonly List<IDomainEvent> _domainEvents = [];
    
    protected AggregateRoot(Guid? id = null): base(id) { }

    public IEnumerable<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();
        
        return copy;
    }

    private AggregateRoot() { }
}