using ErrorOr;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.UnitTests.Common.Rooms;
using SessionReservation.Domain.UnitTests.Common.Sessions;

namespace SessionReservation.Domain.UnitTests.RoomAggregate;

public class RoomTests
{
    [Fact]
    public void ScheduleSession_RoomCapacitySmallerThanSession_ShouldReturnError()
    {
        // Arrange 
        Room room = RoomFactory.CreateRoom(capacity: 10);
        Session session = SessionFactory.CreateSession(capacity: 15);
        
        // Act
        ErrorOr<Created> result = room.ScheduleSession(session);

        // Assert
        Assert.Equal(ErrorType.Forbidden, result.FirstError.Type);
    }
}