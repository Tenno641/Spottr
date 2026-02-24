namespace Spottr.Domain.UnitTests.Common.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(
        Guid? userId = null,
        Guid? id = null)
    {
        Participant participant = new Participant(
            userId: userId ?? Constants.Constants.User.Id,
            id ?? Constants.Constants.Participant.Id
        );

        return participant;
    }
}