namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Session
    {
        public static Guid Id => Guid.CreateVersion7();
        public static int Capacity => 20;
        public static DateOnly Date => DateOnly.MinValue;
        public static Domain.Common.TimeRange TimeRange => new Domain.Common.TimeRange(
            start: Constants.TimeRange.Start,
            end: Constants.TimeRange.End);
    }
}