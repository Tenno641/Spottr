namespace GymManagement.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Admins
    {
        public static Guid Id => Guid.CreateVersion7();
        public static Guid SubscriptionId => Guid.CreateVersion7();
    }
}