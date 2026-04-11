using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.ValueObjects;

namespace SessionReservation.Domain.Equipments;

public class Equipment: Entity
{
    public string Name { get; }
    public Schedule Schedule { get; }

    public Equipment(string name, Schedule? schedule = null, Guid? id = null) : base(id)
    {
        Name = name;
        Schedule = schedule ?? new Schedule();
    }
}