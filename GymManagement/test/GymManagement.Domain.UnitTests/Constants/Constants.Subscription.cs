using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.UnitTests.Constants;

public static partial class Constants
{
    public static class Subscription
    {
        public static Guid Id = Guid.CreateVersion7();
        public static Guid AdminId = Guid.CreateVersion7();
        public static SubscriptionType SubscriptionType = SubscriptionType.Free;
    }
}