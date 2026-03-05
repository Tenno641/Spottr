using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        Guid? trainerId = null,
        int? capacity = null,
        DateOnly? date = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        Guid? id = null)
    {
        Session session = new Session(
            trainerId: trainerId ?? Constants.Constants.Trainer.Id,
            capacity: capacity ?? Constants.Constants.Session.Capacity,
            type: SessionTypes.Cardio,
            id: id ?? Constants.Constants.Session.Id,
            date: date ?? Constants.Constants.Session.Date,
            startTime: startTime ?? Constants.Constants.Session.StartTime,
            endTime: endTime ?? Constants.Constants.Session.EndTime
        );

        return session;
    }
}