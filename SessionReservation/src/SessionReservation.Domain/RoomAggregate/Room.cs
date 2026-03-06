using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private List<Guid> _sessionIds = [];
    private Schedule _schedule = new Schedule();
    private int _dailySessions;

    private int _capacity;

    public Room(int capacity,
        int dailySessions,
        Guid? id = null) : base(id)
    {
        _capacity = capacity;
        _dailySessions = dailySessions;
    }

    public ErrorOr<Created> ScheduleSession(Session session)
    {
        if (_capacity < session.Capacity)
            return RoomErrors.SessionCapacityIsLargerThanTheRoom;

        if (_sessionIds.Count >= _dailySessions)
            return RoomErrors.RoomCannotHaveMoreSessionThanTheSubscriptionAllows;

        if (_schedule.IsTimeSlotOccupied(session.Date, session.TimeRange))
            return RoomErrors.RoomCannotHaveOverlappingSessions;

        _sessionIds.Add(session.Id);
        
        _schedule.BookTimeSlot(session.Date, session.TimeRange);
        
        return Result.Created;
    }
}