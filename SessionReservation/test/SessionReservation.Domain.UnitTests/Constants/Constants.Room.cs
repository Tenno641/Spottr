namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Room
    {
        public static Guid Id => Guid.CreateVersion7();
        public static int Capacity => 20;
    }
}