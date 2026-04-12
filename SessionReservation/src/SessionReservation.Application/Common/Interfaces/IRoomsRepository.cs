using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task<Room?> GetRoomByIdAsync(Guid id);
    Task AddRoomAsync(Room room);
    Task DeleteRoomByIdAsync(Room room);
    Task<List<Room>> ListRoomsByGymIdAsync(Guid gymId);
    Task UpdateRoomAsync(Room room);
}