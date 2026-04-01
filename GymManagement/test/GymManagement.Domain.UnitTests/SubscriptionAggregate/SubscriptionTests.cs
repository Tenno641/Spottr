using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using GymManagement.Domain.UnitTests.Common.Gyms;
using ErrorOr;
using GymManagement.Domain.UnitTests.Common.Subscriptions;

namespace GymManagement.Domain.UnitTests.SubscriptionAggregate;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_CannotHaveMoreGymsThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Subscription subscription = SubscriptionFactory.Create();
        Gym gym1 = GymFactory.CreateGym();
        Gym gym2 = GymFactory.CreateGym();
        
        // Act
        ErrorOr<Created> gym1Result = subscription.AddGym(gym1);
        ErrorOr<Created> gym2Result = subscription.AddGym(gym2);

        // Assert
        Assert.False(gym1Result.IsError);
        Assert.Equal(Result.Created, gym1Result.Value);
        
        Assert.True(gym2Result.IsError);
        Assert.Equal(ErrorType.Forbidden, gym2Result.FirstError.Type);
        Assert.Equal(SubscriptionErrors.CannotHaveMoreGyms, gym2Result.FirstError);
    }
}