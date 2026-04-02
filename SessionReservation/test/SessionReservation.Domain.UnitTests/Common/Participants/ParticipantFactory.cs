using SessionReservation.Domain.ParticipantAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(
        string? name = null,
        int? age = null,
        Guid? id = null)
    {
        Participant participant = new Participant(
            name: name ?? Constants.Constants.Participant.Name,
            age: age ?? Constants.Constants.Participant.Age,
            id: id ?? Constants.Constants.Participant.Id);

        return participant;
    }
}