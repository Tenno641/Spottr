using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Rooms;

public static class RoomFactory
{
    public static Room Create(
        Guid? gymId = null,
        int? capacity = null,
        int? dailySessions = null,
        Guid? id = null)
    {
        Room room = new Room(
            gymId: Constants.Constants.Rooms.GymId,
            capacity: capacity ?? Constants.Constants.Rooms.Capacity,
            maxDailySessions: dailySessions ?? Constants.Constants.Rooms.DailySessions,
            id: id ?? Constants.Constants.Rooms.Id);

        return room;
    }
}