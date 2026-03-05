namespace GymManagement.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Gym
    {
        public static Guid Id => Guid.CreateVersion7();
        public static int MaxRooms => 3;
    }
}