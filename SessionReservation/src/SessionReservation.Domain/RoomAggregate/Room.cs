using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private List<Guid> _sessionId = [];
    private Schedule _schedule = new Schedule();

    private int _capacity;

    public Room(int capacity,
        Guid? id = null) : base(id)
    {
        _capacity = capacity;
    }

    public ErrorOr<Created> ScheduleSession(Session session)
    {
        if (_capacity < session.Capacity)
            return Error.Forbidden(code: "Room.AddSession", description: "Session Capacity is bigger than the room");
        
        



        return Result.Created;
    }
    
    
}