using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.UnitTests.Common.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int? capacity = null,
        Guid? id = null)
    {
        Room room = new Room(
            id: id ?? Constants.Constants.Room.Id);

        return room;
    }
}