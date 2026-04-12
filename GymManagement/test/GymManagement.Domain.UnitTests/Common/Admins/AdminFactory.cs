using GymManagement.Domain.AdminAggregate;

namespace GymManagement.Domain.UnitTests.Common.Admins;

public static class AdminFactory
{
    public static Admin Create(
        Guid? id = null,
        Guid? userId = null)
    {
        Admin admin = new Admin(
            id: id ?? Constants.Constants.Admins.Id,
            userId: userId ?? Constants.Constants.Admins.UserId);

        return admin;
    }
}