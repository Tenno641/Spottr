namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Session
    {
        public static Guid Id => Guid.CreateVersion7();
        public static int Capacity => 20;
    }
}