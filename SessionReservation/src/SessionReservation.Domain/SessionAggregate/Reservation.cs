using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.SessionAggregate;

public class Reservation: Entity
{
    private Guid _participantId;

    public Reservation(Guid participantId,
        Guid? id = null) : base(id)
    {
        _participantId = participantId;
    }
}