using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private List<Guid> _roomIds = [];
    private string _name;
    private int _maxRooms;

    public Gym(
        int maxRooms,
        string name,
        Guid? id = null) : base(id)
    {
        _name = name;
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