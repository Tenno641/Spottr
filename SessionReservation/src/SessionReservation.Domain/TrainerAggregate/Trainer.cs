using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private List<Guid> _sessionId = [];
    private Schedule _schedule;
    private Guid _gymId;

    public Trainer(
        Guid gymId, 
        Schedule? schedule = null, 
        Guid? id = null) : base(id)
    {
        _gymId = gymId;
        _schedule = schedule ?? new Schedule();
    }

    public ErrorOr<Created> TeachSession(Session session)
    {
        if (_sessionId.Contains(session.Id))
            return TrainerErrors.AlreadyTeachesThisSession;

        bool isScheduleOccupied = _schedule.IsTimeSlotOccupied(session.Date, session.TimeRange);
        if (isScheduleOccupied)
            return TrainerErrors.CannotTeachOverlappingSessions;
        
        _schedule.BookTimeSlot(session.Date, session.TimeRange);

        _sessionId.Add(session.Id);
        
        return Result.Created;
    }
    
    public ErrorOr<Deleted> RemoveTrainer(Session session)
    {
        if (!_sessionId.Contains(session.Id))
            return TrainerErrors.SessionNotFound;

        ErrorOr<Deleted> removeFromScheduleResult = _schedule.RemoveBooking(session.Date, session.TimeRange);
        if (removeFromScheduleResult.IsError)
            return removeFromScheduleResult.Errors;
        
        _sessionId.Remove(session.Id);
        return Result.Deleted;
    }

    private Trainer() { }
}