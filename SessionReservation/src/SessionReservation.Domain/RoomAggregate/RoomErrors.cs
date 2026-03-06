using System.Runtime.InteropServices.JavaScript;
using ErrorOr;

namespace SessionReservation.Domain.RoomAggregate;

public static class RoomErrors
{
    public static Error SessionCapacityIsLargerThanTheRoom => Error.Forbidden(code: "Room.AddSession", description: "Session Capacity is bigger than the room");
    
    public static Error RoomCannotHaveMoreSessionThanTheSubscriptionAllows =>
        Error.Forbidden(code: "Room.AddSession", description: "Cannot have more session with this subscription");

    public static Error RoomCannotHaveOverlappingSessions =>
        Error.Conflict(code: "Room.ScheduleSession", description: "Room Cannot Have overlapping sessions");
}