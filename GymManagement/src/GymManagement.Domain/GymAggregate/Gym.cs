using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private List<Guid> _roomIds = [];
    private int _maxRooms;

    public Gym(
        int maxRooms,
        Guid? id = null) : base(id)
    {
        _maxRooms = maxRooms;
    }

    public ErrorOr<Created> AddRoom(Room room)
    {
        if (_roomIds.Count >= _maxRooms)
            return GymErrors.CannotHaveMoreRooms;
        
        _roomIds.Add(room.Id);

        return Result.Created;
    }
}