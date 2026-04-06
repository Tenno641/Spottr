using ErrorOr;
using GymManagement.Domain.AdminAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using GymManagement.Domain.UnitTests.Common.Admins;
using GymManagement.Domain.UnitTests.Common.Subscriptions;

namespace GymManagement.Domain.UnitTests.AddminAggregate;

public class AdminTests
{
    [Fact]
    public void SetSubscription_AdminAlreadyHasSubscription_ShouldFail()
    {
        // Arrange
        Admin admin = AdminFactory.Create();
        Subscription subscription1 = SubscriptionFactory.Create();
        Subscription subscription2 = SubscriptionFactory.Create();
        
        // Act
        ErrorOr<Success> subscription1Result = admin.SetSubscription(subscription1);
        ErrorOr<Success> subscription2Result = admin.SetSubscription(subscription2);

        // Assert
        Assert.False(subscription1Result.IsError);
        Assert.Equal(Result.Success, subscription1Result.Value);
        
        Assert.True(subscription2Result.IsError);
        Assert.Equal(ErrorType.Conflict, subscription2Result.FirstError.Type);
        Assert.Equal(AdminErrors.SubscriptionAlreadyExists, subscription2Result.FirstError);
    }
}