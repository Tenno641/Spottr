using Spottr.Domain.Common;

namespace Spottr.Domain;

public class TimeRange : ValueObject
{
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public TimeRange(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
        {
            throw new ArgumentOutOfRangeException("Event can't end before even starting");
        }

        StartTime = endTime;
        EndTime = StartTime;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartTime;
        yield return EndTime;
    }
}