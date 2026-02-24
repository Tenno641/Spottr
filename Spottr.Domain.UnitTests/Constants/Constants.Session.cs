namespace Spottr.Domain.UnitTests.Constants;

public static partial class Constants
{
    public static class Session
    {
        public readonly static Guid Id = Guid.CreateVersion7();
        public readonly static DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);
        public readonly static TimeOnly Start = TimeOnly.MinValue;
        public readonly static TimeOnly End = TimeOnly.MaxValue;
        
    }
}