using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.UnitTests.Common.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription Create(
        Guid? id = null,
        Guid? adminId = null,
        SubscriptionType? subscriptionType = null)
    {
        Subscription subscription = new Subscription(
            id: id ?? Constants.Constants.Subscription.Id,
            adminId: adminId ?? Constants.Constants.Subscription.AdminId,
            subscriptionType: subscriptionType ?? Constants.Constants.Subscription.SubscriptionType);

        return subscription;
    }
}