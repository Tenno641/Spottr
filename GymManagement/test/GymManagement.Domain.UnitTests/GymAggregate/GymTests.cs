using ErrorOr;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.UnitTests.Common.Gyms;
using GymManagement.Domain.UnitTests.Common.Rooms;

namespace GymManagement.Domain.UnitTests.GymAggregate;

public class GymTests
{
    [Fact]
    public void AddRoom_CannotAddRoomThanTheSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Gym gym = GymFactory.CreateGym(maxRooms:1);
        Room room1 = RoomFactory.CreateRoom(id: Guid.CreateVersion7());
        Room room2 = RoomFactory.CreateRoom(id: Guid.CreateVersion7());
        
        // Act
        ErrorOr<Created> room1Result = gym.AddRoom(room1);
        ErrorOr<Created> room2Result = gym.AddRoom(room2);

        // Assert
        Assert.False(room1Result.IsError);
        Assert.Equal(Result.Created, room1Result.Value);
        
        Assert.True(room2Result.IsError);
        Assert.Equal(GymErrors.CannotHaveMoreRooms, room2Result.FirstError);
        Assert.Equal(ErrorType.Forbidden, room2Result.FirstError.Type);
    }
}