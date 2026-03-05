using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private string _name;
    private List<Guid> _sessionIds = [];
    private Schedule _schedule = new Schedule();

    public Participant(string name, Guid? id = null) : base(id)
    {
        _name = name;
    }
}