namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Session
    {
        public static Guid Id => Guid.CreateVersion7();
        public static int Capacity => 20;
        public static DateOnly Date => DateOnly.MinValue;
        public static TimeOnly StartTime => TimeOnly.MinValue.AddHours(5);
        public static TimeOnly EndTime => TimeOnly.MinValue.AddHours(10);
    }
}