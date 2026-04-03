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
            name: name ?? Constants.Constants.Participants.Name,
            age: age ?? Constants.Constants.Participants.Age,
            id: id ?? Constants.Constants.Participants.Id);

        return participant;
    }
}