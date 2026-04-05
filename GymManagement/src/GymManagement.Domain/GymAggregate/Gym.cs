using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private List<Guid> _roomIds = [];
    public string Name { get; }
    private int _maxRooms;
    private List<Guid> _trainersIds = [];
    
    public Guid SubscriptionId { get; }

    public Gym(
        Guid subscriptionId,
        int maxRooms,
        string name,
        Guid? id = null) : base(id)
    {
        SubscriptionId = subscriptionId;
        Name = name;
        _maxRooms = maxRooms;
    }

    public ErrorOr<Created> AddRoom(Room room)
    {
        if (_roomIds.Count >= _maxRooms)
            return GymErrors.CannotHaveMoreRooms;
        
        _roomIds.Add(room.Id);

        return Result.Created;
    }

    public ErrorOr<Success> AddTrainer(Guid trainerId)
    {
        if (_trainersIds.Contains(trainerId))
            return Error.Conflict(description: "Trainer already added to the gym");
        
        _trainersIds.Add(trainerId);
        return Result.Success;
    }
}