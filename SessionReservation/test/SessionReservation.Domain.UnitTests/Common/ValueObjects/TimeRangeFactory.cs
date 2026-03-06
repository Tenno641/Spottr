using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.UnitTests.Common.ValueObjects;

public static class TimeRangeFactory
{
    public static TimeRange CreateTimeRange(
        TimeOnly? start = null,
        TimeOnly? end = null)
    {
        TimeRange timeRange = new TimeRange(start: start ?? Constants.Constants.TimeRange.Start,
            end: end ?? Constants.Constants.TimeRange.End);

        return timeRange;
    }
}