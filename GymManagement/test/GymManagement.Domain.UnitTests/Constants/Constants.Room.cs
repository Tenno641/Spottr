namespace GymManagement.Domain.UnitTests.Constants;

public static partial class Constants
{
    public static class Room
    {
        public static Guid Id => Guid.CreateVersion7();
        public static Guid GymId => Guid.CreateVersion7();
        public static int MaxDailySessions => 3;
        public static int Capacity => 3;
    }
}