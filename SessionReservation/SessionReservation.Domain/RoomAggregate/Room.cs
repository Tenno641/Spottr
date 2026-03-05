using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private List<Guid> _sessionId = [];
    private Schedule _schedule = new Schedule();

    public Room(Guid? id = null) : base(id)
    {
        
    }
}