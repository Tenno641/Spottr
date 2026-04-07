using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate.Events;

namespace GymManagement.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private List<Guid> _gymIds = [];
    private int _maxGyms;
    private Guid _adminId;
    private SubscriptionType SubscriptionType { get; }

    public Subscription(
        Guid adminId,
        SubscriptionType subscriptionType,
        Guid? id = null) : base(id)
    {
        _maxGyms = GetMaxGyms();
        _adminId = adminId;
        SubscriptionType = subscriptionType;
    }
   
    public ErrorOr<Created> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id))
            return SubscriptionErrors.GymIsAlreadyAdded;
        
        if (_gymIds.Count >= _maxGyms)
            return SubscriptionErrors.CannotHaveMoreGyms;

        _gymIds.Add(gym.Id);
        
        _domainEvents.Add(new GymAddedEvent(gym));
        
        return Result.Created;
    }

    public ErrorOr<Deleted> RemoveGym(Guid gymId)
    {
        if (!_gymIds.Contains(gymId))
            return SubscriptionErrors.GymIsNotFound;
        
        _gymIds.Remove(gymId);
        
        _domainEvents.Add(new GymDeletedEvent(gymId));
        
        return Result.Deleted;
    }
    
    private int GetMaxGyms()
    {
        return SubscriptionType switch
        {
            SubscriptionType.Free => 1,
            SubscriptionType.Starter => 3,
            SubscriptionType.Premium => int.MaxValue,
            _ => throw new ArgumentOutOfRangeException(nameof(SubscriptionType), SubscriptionType, null)
        };
    }
}