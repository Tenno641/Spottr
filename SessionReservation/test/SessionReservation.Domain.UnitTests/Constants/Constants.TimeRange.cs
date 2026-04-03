namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class TimeRanges
    {
        public static TimeOnly Start => TimeOnly.MinValue.AddHours(5);
        public static TimeOnly End => TimeOnly.MinValue.AddHours(10);
    }
}