using GymManagement.Domain.Common;

namespace GymManagement.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private List<Guid> _gymIds = [];

    public Subscription(Guid? id = null) : base(id)
    {
        
    }
}