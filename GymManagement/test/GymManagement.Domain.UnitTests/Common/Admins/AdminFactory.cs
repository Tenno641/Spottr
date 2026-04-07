using GymManagement.Domain.AdminAggregate;

namespace GymManagement.Domain.UnitTests.Common.Admins;

public static class AdminFactory
{
    public static Admin Create(
        Guid? id = null,
        Guid? subscriptionId = null)
    {
        Admin admin = new Admin(
            id: id ?? Constants.Constants.Admins.Id,
            subscriptionId: subscriptionId ?? Constants.Constants.Admins.SubscriptionId);

        return admin;
    }
}