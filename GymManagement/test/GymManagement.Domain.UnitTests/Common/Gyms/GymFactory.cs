using GymManagement.Domain.GymAggregate;

namespace GymManagement.Domain.UnitTests.Common.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        int? maxRooms = null,
        Guid? id = null
        )
    {
        Gym gym = new Gym(
            maxRooms: maxRooms ?? Constants.Constants.Gym.MaxRooms,
            id: id ??Constants.Constants.Gym.Id);

        return gym;
    }
}