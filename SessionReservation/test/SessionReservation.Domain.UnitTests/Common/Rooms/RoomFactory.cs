using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int? capacity = null,
        int? dailySessions = null,
        Guid? id = null)
    {
        Room room = new Room(
            capacity: capacity ?? Constants.Constants.Room.Capacity,
            dailySessions: dailySessions ?? Constants.Constants.Room.DailySessions,
            id: id ?? Constants.Constants.Room.Id);

        return room;
    }
}