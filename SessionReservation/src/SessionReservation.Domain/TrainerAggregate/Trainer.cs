using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private List<Guid> _sessionId = [];
    private Schedule _schedule;

    public Trainer(Schedule? schedule = null, Guid? id = null) : base(id)
    {
        _schedule = schedule ?? new Schedule();
    }

    public ErrorOr<Created> TeachSession(Session session)
    {
        if (_sessionId.Contains(session.Id))
            return TrainerErrors.AlreadyTeachesThisSession;

        if (_schedule.IsTimeSlotOccupied(session.Date, session.TimeRange))
            return TrainerErrors.CannotTeachOverlappingSessions;

        _schedule.BookTimeSlot(session.Date, session.TimeRange);
        _sessionId.Add(session.Id);
        return Result.Created;
    }
}