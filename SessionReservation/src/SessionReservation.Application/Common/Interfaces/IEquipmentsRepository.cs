using SessionReservation.Domain.Common.Entities;

namespace SessionReservation.Application.Common.Interfaces;

public interface IEquipmentsRepository
{
    Task<List<Equipment>> GetEquipmentsByIds(List<Guid> equipmentIds);
    Task UpdateEquipmentsAsync(List<Equipment> equipments);
}