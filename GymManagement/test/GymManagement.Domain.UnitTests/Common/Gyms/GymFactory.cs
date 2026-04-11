using GymManagement.Domain.Common.Entities;
using GymManagement.Domain.GymAggregate;

namespace GymManagement.Domain.UnitTests.Common.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        Guid? subscriptionId = null,
        string? name = null,
        List<Equipment>? equipments = null,
        int? maxRooms = null,
        Guid? id = null
        )
    {
        Gym gym = new Gym(
            subscriptionId: subscriptionId ?? Constants.Constants.Gym.SubscriptionId,
            equipments: equipments ?? Constants.Constants.Gym.Equipments,
            name: name ?? Constants.Constants.Gym.Name,
            maxRooms: maxRooms ?? Constants.Constants.Gym.MaxRooms,
            id: id ??Constants.Constants.Gym.Id);

        return gym;
    }
}