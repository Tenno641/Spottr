using ErrorOr;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.Common.Entities;

public class Equipment: Entity
{
    public string Name { get; }
    public Schedule Schedule { get; }

    public Equipment(string name, Schedule? schedule = null, Guid? id = null) : base(id)
    {
        Name = name;
        Schedule = schedule ?? new Schedule();
    }

    public ErrorOr<Success> RemoveFromSchedule(Session session)
    {
        if (session.Equipments.All(e => e.Id != Id))
            return Error.NotFound("Equipment.RemoveFromSchedule", description: "Session doesn't use this equipment");

        ErrorOr<Deleted> result = Schedule.RemoveBooking(session.Date, session.TimeRange);

        if (result.IsError)
            return result.Errors;

        return Result.Success;
    }
}