using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private string _name;
    private List<Guid> _sessionIds = [];
    private readonly Schedule _schedule;

    public Participant(string name, Schedule? schedule = null, Guid? id = null) : base(id)
    {
        _name = name;
        _schedule = schedule ?? new Schedule();
    }

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        ErrorOr<Created> result = _schedule.BookTimeSlot(session.Date, session.TimeRange);

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