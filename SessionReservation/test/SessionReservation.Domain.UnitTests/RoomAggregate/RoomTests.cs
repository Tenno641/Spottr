using ErrorOr;
using SessionReservation.Domain.Equipments;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.UnitTests.Common.Equipments;
using SessionReservation.Domain.UnitTests.Common.Rooms;
using SessionReservation.Domain.UnitTests.Common.Sessions;

namespace SessionReservation.Domain.UnitTests.RoomAggregate;

public class RoomTests
{
    [Fact]
    public void ScheduleSession_RoomCapacitySmallerThanSession_ShouldReturnError()
    {
        // Arrange 
        Room room = RoomFactory.Create(capacity: 10);
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
        Room room = RoomFactory.Create(dailySessions: 1);
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

    [Fact]
    public void ScheduleSession_SessionsTimeConflict_ShouldFail()
    {
        // Arrange 
        Room room = RoomFactory.Create();
        Session session1 = SessionFactory.CreateSession();
        Session session2 = SessionFactory.CreateSession();

        // Act
        ErrorOr<Created> result1 = room.ScheduleSession(session1);
        ErrorOr<Created> result2 = room.ScheduleSession(session2);

        // Assert
        Assert.Equal(Result.Created, result1.Value);
        Assert.Equal(ErrorType.Conflict, result2.FirstError.Type);
        Assert.Equal(RoomErrors.RoomCannotHaveOverlappingSessions, result2.FirstError);
    }

    [Fact]
    public void ScheduleSession_RequiredEquipmentsWillBeUsed_ShouldFail()
    {
        // Arrange
        // Equipments
        Equipment equipment1 = EquipmentFactory.Create(id: Guid.CreateVersion7(), name: "Equipment-Name-1");
        Equipment equipment2 = EquipmentFactory.Create(id: Guid.CreateVersion7(), name: "Equipment-Name-2");
        Equipment equipment3 = EquipmentFactory.Create(id: Guid.CreateVersion7(), name: "Equipment-Name-3");
        List<Equipment> requiredEquipments = [equipment1, equipment2, equipment3];

        Guid gymId = Guid.CreateVersion7();
        Room room1 = RoomFactory.Create(gymId: gymId);
        Room room2 = RoomFactory.Create(gymId: gymId);
        Session session1 = SessionFactory.CreateSession(equipments: requiredEquipments);
        Session session2 = SessionFactory.CreateSession(equipments: requiredEquipments);
        
        // Act
        ErrorOr<Created> session1Result = room1.ScheduleSession(session1);
        ErrorOr<Created> session2Result = room2.ScheduleSession(session2);
        
        // Assert
        Assert.False(session1Result.IsError);
        Assert.Equal(Result.Created, session1Result.Value);
        
        Assert.True(session2Result.IsError);
        Assert.Equal(ErrorType.Conflict, session2Result.FirstError.Type);
        Assert.Equal(RoomErrors.EquipmentsWillNotBeAvailableForThisSession, session2Result.FirstError);
    }
}