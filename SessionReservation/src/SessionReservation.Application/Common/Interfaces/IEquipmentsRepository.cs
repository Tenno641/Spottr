using SessionReservation.Domain.Equipments;

namespace SessionReservation.Application.Common.Interfaces;

public interface IEquipmentsRepository
{
    Task<List<Equipment>> GetEquipmentsById(List<Guid> ids);
    Task<Equipment?> GetEquipmentById(Guid id);
}