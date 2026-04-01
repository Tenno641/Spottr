using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.UnitTests.Common.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription Create()
    {
        Subscription subscription = new Subscription(
            id: Constants.Constants.Subscription.Id,
            maxGyms: Constants.Constants.Subscription.MaxGyms);

        return subscription;
    }
}