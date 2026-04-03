using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.Equipments;

public class Equipment: Entity
{
    private string _name;
    public Schedule Schedule { get; }

    public Equipment(string name, Schedule? schedule = null, Guid? id = null) : base(id)
    {
        _name = name;
        Schedule = schedule ?? new Schedule();
    }
}