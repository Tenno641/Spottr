using GymManagement.Domain.Common;

namespace GymManagement.Domain.AdminAggregate;

public class Admin : AggregateRoot
{
    private Guid _subscriptionId;

    public Admin(Guid subscriptionId,
        Guid? id = null) : base(id)
    {
        _subscriptionId = subscriptionId;
    }
}
