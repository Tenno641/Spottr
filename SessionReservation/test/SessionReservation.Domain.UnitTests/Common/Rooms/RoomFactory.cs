using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int? capacity = null,
        Guid? id = null)
    {
        Room room = new Room(
            capacity: capacity ?? Constants.Constants.Room.Capacity,
            id: id ?? Constants.Constants.Room.Id);

        return room;
    }
}