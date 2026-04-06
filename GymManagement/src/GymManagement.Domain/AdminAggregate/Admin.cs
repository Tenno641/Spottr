using ErrorOr;
using GymManagement.Domain.AdminAggregate.Events;
using GymManagement.Domain.Common;
using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    private Guid? _subscriptionId;

    public Admin(Guid subscriptionId,
        Guid? id = null) : base(id)
    {
        _subscriptionId = subscriptionId;
    }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (_subscriptionId is not null)
            return Error.Conflict("Subscription already exists");
        
        _subscriptionId = subscription.Id;
        
        _domainEvents.Add(new SubscriptionSetEvent(subscription));
        // TODO: Listen to event and add subscription to the database

        return Result.Success;
    }
}
