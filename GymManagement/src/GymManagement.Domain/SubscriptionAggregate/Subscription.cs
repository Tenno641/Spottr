using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.GymAggregate;

namespace GymManagement.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private List<Guid> _gymIds = [];
    private int _maxGyms;

    public Subscription(int maxGyms, Guid? id = null) : base(id)
    {
        _maxGyms = maxGyms;
    }

    public ErrorOr<Created> AddGym(Gym gym)
    {
        if (_gymIds.Count >= _maxGyms)
            return SubscriptionErrors.CannotHaveMoreGyms;

        _gymIds.Add(gym.Id);
        
        return Result.Created;
    }
}