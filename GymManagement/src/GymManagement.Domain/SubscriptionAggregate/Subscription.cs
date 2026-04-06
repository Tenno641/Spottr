using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.GymAggregate;

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
            throw new Exception("Gym is already added");
        
        if (_gymIds.Count >= _maxGyms)
            return SubscriptionErrors.CannotHaveMoreGyms;

        _gymIds.Add(gym.Id);
        
        return Result.Created;
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