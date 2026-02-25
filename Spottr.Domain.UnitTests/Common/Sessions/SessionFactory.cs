namespace Spottr.Domain.UnitTests.Common.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        int maxParticipants,
        DateOnly? date = null,
        TimeRange? timeRange = null,
        Guid? trainerId = null,
        Guid? id = null
    )
    {
        TimeRange timeRangeObj = new TimeRange(

            startTime: timeRange?.StartTime ?? Constants.Constants.Session.Start,
            endTime: timeRange?.EndTime ?? Constants.Constants.Session.End);

        Session session = new Session(
            maxParticipants: maxParticipants,
            date: date ?? Constants.Constants.Session.Date,
            timeRange: timeRangeObj,
            trainerId: trainerId ?? Constants.Constants.Trainer.Id,
            id: id ?? Constants.Constants.Session.Id
        );

        return session;
    }
}