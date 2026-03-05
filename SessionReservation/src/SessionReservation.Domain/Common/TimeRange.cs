namespace SessionReservation.Domain.Common;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    
    public TimeRange(TimeOnly end, TimeOnly start)
    {
        Start = start;
        End = end;
    }
    
    protected override IEnumerable<object> GetProperties()
    {
        yield return Start;
        yield return End;
    }
}