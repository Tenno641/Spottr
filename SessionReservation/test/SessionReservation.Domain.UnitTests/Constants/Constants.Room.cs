namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Rooms
    {
        public static Guid Id => Guid.CreateVersion7();
        public static Guid GymId => Guid.CreateVersion7();
        public static int Capacity => 20;
        public static int DailySessions => 3;
    }
}