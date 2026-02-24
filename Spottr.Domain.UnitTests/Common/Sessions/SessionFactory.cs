namespace Spottr.Domain.UnitTests.Common.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        int maxParticipants,
        DateOnly? date = null,
        TimeOnly? startTime = null,
        TimeOnly? endTime = null,
        Guid? trainerId = null,
        Guid? id = null
    )
    {
        Session session = new Session(
            maxParticipants: maxParticipants,
            date: date ?? Constants.Constants.Session.Date,
            startTime: startTime ?? Constants.Constants.Session.Start,
            endTime: endTime ?? Constants.Constants.Session.End,
            trainerId: trainerId ?? Constants.Constants.Trainer.Id,
            id: id ?? Constants.Constants.Session.Id
        );

        return session;
    }
}