using SessionReservation.Domain.ParticipantAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant()
    {
        Participant participant = new Participant(
            name: Constants.Constants.Participant.Name,
            id: Constants.Constants.Participant.Id);

        return participant;
    }
}