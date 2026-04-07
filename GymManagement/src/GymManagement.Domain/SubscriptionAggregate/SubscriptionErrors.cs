using ErrorOr;

namespace GymManagement.Domain.SubscriptionAggregate;

public static class SubscriptionErrors
{
    public static Error GymIsNotFound = Error.NotFound("Subscription.AddGym/RemoveGym", "Gym is not found");
    public static Error CannotHaveMoreGyms = Error.Forbidden("Subscription.AddGym", "Cannot have more gyms than subscription allows");
    public static Error GymIsAlreadyAdded = Error.Forbidden("Subscription.AddGym", "Gym is already added");
}