using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.UnitTests.Common.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int? maxDailySessions = null,
        Guid? gymId = null,
        int? capacity = null,
        string? name = null,
        Guid? id = null)
    {
        Room room = new Room(
            gymId: gymId ?? Constants.Constants.Room.GymId,
            name: name ?? Constants.Constants.Room.Name,
            maxDailySessions: maxDailySessions ?? Constants.Constants.Room.MaxDailySessions,
            capacity: capacity ?? Constants.Constants.Room.Capacity,
            id: id ?? Constants.Constants.Room.Id);

        return room;
    }
}