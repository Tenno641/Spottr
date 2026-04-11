using SessionReservation.Domain.Common.Entities;

namespace SessionReservation.Domain.UnitTests.Common.Equipments;

public static class EquipmentFactory
{
    public static Equipment Create(
        string? name = null,
        Guid? id = null)
    {
        Equipment equipment = new Equipment(
            id: id ?? Constants.Constants.Equipments.Id,
            name: name ?? Constants.Constants.Equipments.Name);
        
        return equipment;
    }
}