using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.RoomAggregate.Events;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private List<Guid> _sessionIds = [];
    private Schedule _schedule = new Schedule();
    private int _maxDailySessions;
    public int Capacity { get; }

    public IReadOnlyList<Guid> SessionIds => _sessionIds;
    public Guid GymId { get; private set; }
    public string Name { get; private set; }

    public Room(int capacity,
        int maxDailySessions,
        string name,
        Guid gymId,
        Guid? id = null) : base(id)
    {
        Capacity = capacity;
        _maxDailySessions = maxDailySessions;
        Name = name;
        GymId = gymId;
    }

    public ErrorOr<Created> ScheduleSession(Session session)
    {
        if (Capacity < session.Capacity)
            return RoomErrors.SessionCapacityIsLargerThanTheRoom;

        if (_sessionIds.Count >= _maxDailySessions)
            return RoomErrors.RoomCannotHaveMoreSessionThanTheSubscriptionAllows;

        bool isScheduleOccupied = _schedule.IsTimeSlotOccupied(session.Date, session.TimeRange);
        if (isScheduleOccupied)
            return RoomErrors.RoomCannotHaveOverlappingSessions;

        if (session.Equipments.Any(equipment => equipment.Schedule.IsTimeSlotOccupied(session.Date, session.TimeRange)))
            return RoomErrors.EquipmentsWillNotBeAvailableForThisSession;

        _schedule.BookTimeSlot(session.Date, session.TimeRange);
        
        _sessionIds.Add(session.Id);
        
        foreach (Equipment equipment in session.Equipments)
        {
            equipment.Schedule.BookTimeSlot(session.Date, session.TimeRange);
        }
        
        _domainEvents.Add(new SessionScheduledEvent(session));
        
        return Result.Created;
    }

    public ErrorOr<Success> CancelSession(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
            return RoomErrors.SessionIsNotFound;
        
        _sessionIds.Remove(session.Id);
        
        _schedule.RemoveBooking(session.Date, session.TimeRange);
        
        _domainEvents.Add(new SessionCancelledEvent(session));
        
        return Result.Success;
    }
    
    private Room() { }
}