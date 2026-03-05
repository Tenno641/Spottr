using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private Guid _trainerId;
    private List<Reservation> _reservations = [];
    private readonly SessionTypes _type;
    
    public int Capacity { get; }

    public Session(Guid trainerId,
        int? capacity,
        SessionTypes type,
        Guid? id = null) : base(id)
    {
        _trainerId = trainerId;
        Capacity = capacity ?? GetCapacityByType();
        _type = type;
    }

    private int GetCapacityByType()
    {
        return _type switch
        {
            SessionTypes.Cardio => 15,
            SessionTypes.Strength => 7,
            SessionTypes.Functional => 5,
            _ => 20
        };
    }
}