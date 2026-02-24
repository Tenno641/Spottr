namespace Spottr.Domain.UnitTests.Common.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        int maxParticipants,
        Guid? trainerId = null,
        Guid? id = null
    )
    {
        Session session = new Session(
            maxParticipants: maxParticipants,
            trainerId: trainerId ?? Constants.Constants.Trainer.Id,
            id: id ?? Constants.Constants.Session.Id
        );

        return session;
    }
}