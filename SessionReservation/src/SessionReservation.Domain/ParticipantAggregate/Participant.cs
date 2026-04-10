using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private string _name;
    private List<Guid> _sessionIds = [];
    private readonly Schedule _schedule;
    
    public int Age { get; }
    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    public Participant(string name, int age, Schedule? schedule = null, Guid? id = null) : base(id)
    {
        _name = name;
        _schedule = schedule ?? new Schedule();
        Age = age;
    }

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return ParticipantErrors.AlreadyReservedThisSession;

        bool isScheduleOccupied = _schedule.IsTimeSlotOccupied(session.Date, session.TimeRange);
        
        if (isScheduleOccupied)
            return ParticipantErrors.ParticipantHasOverlappingSessions;
        
        _schedule.BookTimeSlot(session.Date, session.TimeRange);

        _sessionIds.Add(session.Id);

        return Result.Success;
    }

    public ErrorOr<Success> CancelSession(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
            return ParticipantErrors.SessionIsNotFound;
        
        bool isDeleted = _sessionIds.Remove(session.Id);
        
        if (!isDeleted)
            return Error.Failure(description: "Couldn't delete session");
        
        ErrorOr<Deleted> removeBookingResult = _schedule.RemoveBooking(session.Date, session.TimeRange);

        if (removeBookingResult.IsError)
            return removeBookingResult.Errors;
        
        return Result.Success;
    }

    private Participant() { }
}