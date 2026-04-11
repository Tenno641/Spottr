using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;

namespace SessionReservation.Domain.Gyms;

public class Gym : Entity
{
    public List<Equipment> Equipments { get; }
    
    public Gym(List<Equipment> equipments)
    {
        Equipments = equipments;
    }

    private Gym() { }
}