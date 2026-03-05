using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        Guid? trainerId = null,
        int? capacity = null,
        DateOnly? date = null,
        TimeRange? timeRange = null,
        Guid? id = null)
    {
        Session session = new Session(
            trainerId: trainerId ?? Constants.Constants.Trainer.Id,
            capacity: capacity ?? Constants.Constants.Session.Capacity,
            type: SessionTypes.Cardio,
            id: id ?? Constants.Constants.Session.Id,
            date: date ?? Constants.Constants.Session.Date,
            timeRange: timeRange ?? Constants.Constants.Session.TimeRange
        );

        return session;
    }
}