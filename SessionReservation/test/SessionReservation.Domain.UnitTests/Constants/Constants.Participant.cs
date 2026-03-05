namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Participant
    {
        public static Guid Id => Guid.CreateVersion7();
        public static string Name => "Participant-Name";
    }
}