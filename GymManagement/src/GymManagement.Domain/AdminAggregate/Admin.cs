using ErrorOr;
using GymManagement.Domain.AdminAggregate.Events;
using GymManagement.Domain.Common;
using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    private Guid? _subscriptionId;
    
    public Guid UserId { get; }

    public Admin(Guid userId, Guid? id = null) : base(id)
    {
        UserId = userId;
    }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (_subscriptionId is not null)
            return AdminErrors.SubscriptionAlreadyExists;
        
        _subscriptionId = subscription.Id;
        
        _domainEvents.Add(new SubscriptionSetEvent(subscription));

        return Result.Success;
    }
    
    private Admin() { }
}
