using SessionReservation.Domain.Common.Entities;

namespace SessionReservation.Application.Common.Interfaces;

public interface IEquipmentsRepository
{
    Task<List<Equipment>> GetEquipmentsByIds(Guid gymId, List<Guid> ids);
}