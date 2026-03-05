using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.SessionAggregate;

public class Reservation: Entity
{
    public Guid ParticipantId { get; }

    public Reservation(Guid participantId,
        Guid? id = null) : base(id)
    {
        ParticipantId = participantId;
    }
}