using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private string _name;
    private List<Guid> _sessionIds = [];
    public Schedule Schedule => new Schedule();

    public Participant(string name, Guid? id = null) : base(id)
    {
        _name = name;
    }

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        ErrorOr<Created> result = Schedule.BookTimeSlot(session.Date, session.TimeRange);

        if (result.IsError)
        {
            return result.FirstError.Type == ErrorType.Conflict
                ? ParticipantErrors.ParticipantHasOverlappingSessions
                : result.Errors;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}