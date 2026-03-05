using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private List<Guid> _sessionIds = [];
    private Schedule _schedule = new Schedule();

    public Participant(Guid? id = null) : base(id)
    {
        
    }
}