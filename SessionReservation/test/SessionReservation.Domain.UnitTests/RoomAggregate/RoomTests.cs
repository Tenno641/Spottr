using System.Diagnostics;
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

    [Fact]
    public void ScheduleSession_CannotHaveMoreSessionThanTheSubscriptionAllows()
    {
        // Arrange
        Room room = RoomFactory.CreateRoom(dailySessions: 1);
        Session session1 = SessionFactory.CreateSession();
        Session session2 = SessionFactory.CreateSession();

        // Act
        ErrorOr<Created> result1 = room.ScheduleSession(session1);
        ErrorOr<Created> result2 = room.ScheduleSession(session2);

        // Assert
        Assert.Equal(Result.Created, result1.Value);
        Assert.Equal(Result.Created, result2.Value);
        Assert.Equal(ErrorType.Forbidden, result2.FirstError.Type);
        
        Assert.Equal(RoomErrors.RoomCannotHaveMoreSessionThanTheSubscriptionAllows, result2.FirstError);
    }
}