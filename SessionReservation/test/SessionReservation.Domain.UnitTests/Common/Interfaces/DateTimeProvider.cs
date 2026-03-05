using SessionReservation.Domain.Common.Interfaces;

namespace SessionReservation.Domain.UnitTests.Common.Interfaces;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow { get; }
    
    public DateTimeProvider(DateTime? fixedTime)
    {
        UtcNow = fixedTime ?? DateTime.UtcNow;
    }
}