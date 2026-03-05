using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private Guid _trainerId;
    private List<Reservation> _reservations = [];

    public Session(Guid trainerId,
        Guid? id = null) : base(id)
    {
        _trainerId = trainerId;
    }
}