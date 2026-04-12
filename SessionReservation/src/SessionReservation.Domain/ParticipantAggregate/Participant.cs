using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly Schedule _schedule;
    
    public List<Guid> SessionIds { get; }
    public string Name { get; private set; }
    public int Age { get; private set; }

    public Participant(string name, int age, Schedule? schedule = null, Guid? id = null) : base(id)
    {
        Name = name;
        _schedule = schedule ?? new Schedule();
        Age = age;
        SessionIds = [];
    }

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        if (SessionIds.Contains(session.Id))
            return ParticipantErrors.AlreadyReservedThisSession;

        bool isScheduleOccupied = _schedule.IsTimeSlotOccupied(session.Date, session.TimeRange);
        
        if (isScheduleOccupied)
            return ParticipantErrors.ParticipantHasOverlappingSessions;
        
        _schedule.BookTimeSlot(session.Date, session.TimeRange);

        SessionIds.Add(session.Id);

        return Result.Success;
    }

    public ErrorOr<Success> CancelSession(Session session)
    {
        if (!SessionIds.Contains(session.Id))
            return ParticipantErrors.SessionIsNotFound;
        
        bool isDeleted = SessionIds.Remove(session.Id);
        
        if (!isDeleted)
            return Error.Failure(description: "Couldn't delete session");
        
        ErrorOr<Deleted> removeBookingResult = _schedule.RemoveBooking(session.Date, session.TimeRange);

        if (removeBookingResult.IsError)
            return removeBookingResult.Errors;
        
        return Result.Success;
    }

    private Participant() { }
}