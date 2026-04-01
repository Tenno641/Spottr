using ErrorOr;

namespace GymManagement.Domain.GymAggregate;

public static class GymErrors
{
    public static Error CannotHaveMoreRooms = Error.Forbidden("Gym.AddRoome", "Subscription cannot allow more rooms");
}