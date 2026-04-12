namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Trainers
    {
        public static Guid Id => Guid.CreateVersion7();
        public static Guid UserId => Guid.CreateVersion7();
        public static string Name => "Trainer-Name";
    }
}