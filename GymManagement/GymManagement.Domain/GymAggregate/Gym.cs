using GymManagement.Domain.Common;

namespace GymManagement.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private List<Guid> _roomIds = [];

    public Gym(Guid? id = null) : base(id)
    {
        
    }
}