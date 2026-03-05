using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private List<Guid> _sessionId = [];
    private Schedule _schedule = new Schedule();

    public Trainer(Guid? id = null) : base(id)
    {

    }
}