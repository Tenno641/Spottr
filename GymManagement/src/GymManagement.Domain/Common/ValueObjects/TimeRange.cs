namespace GymManagement.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    
    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }
    
    public bool OverlapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        return other.Start < End;
    }
    
    protected override IEnumerable<object> GetProperties()
    {
        yield return Start;
        yield return End;
    }
}