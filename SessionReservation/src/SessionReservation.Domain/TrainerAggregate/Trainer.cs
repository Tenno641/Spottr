using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private List<Guid> _sessionIds = [];
    private Schedule _schedule;
    
    public Guid UserId { get; }
    public string Name { get; }
    
    public Trainer(
        Guid userId,
        string name,
        Schedule? schedule = null, 
        Guid? id = null) : base(id)
    {
        UserId = userId;
        _schedule = schedule ?? new Schedule();
        Name = name;
    }

    public ErrorOr<Created> TeachSession(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return TrainerErrors.AlreadyTeachesThisSession;

        bool isScheduleOccupied = _schedule.IsTimeSlotOccupied(session.Date, session.TimeRange);
        if (isScheduleOccupied)
            return TrainerErrors.CannotTeachOverlappingSessions;
        
        _schedule.BookTimeSlot(session.Date, session.TimeRange);

        _sessionIds.Add(session.Id);
        
        return Result.Created;
    }
    
    public ErrorOr<Deleted> RemoveTrainer(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
            return TrainerErrors.SessionNotFound;

        ErrorOr<Deleted> removeFromScheduleResult = _schedule.RemoveBooking(session.Date, session.TimeRange);
        if (removeFromScheduleResult.IsError)
            return removeFromScheduleResult.Errors;
        
        _sessionIds.Remove(session.Id);
        
        return Result.Deleted;
    }

    private Trainer() { }
}