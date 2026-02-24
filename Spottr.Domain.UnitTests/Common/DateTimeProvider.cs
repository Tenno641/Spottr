using Spottr.Domain.Services;

namespace Spottr.Domain.UnitTests.Common;

public class DateTimeProvider : IDateTimeProvider
{
    private readonly DateTime? _fixedTime;
    
    public DateTimeProvider(DateTime? fixedTime = null)
    {
        _fixedTime = fixedTime;
    }

    public DateTime UtcNow => _fixedTime ?? DateTime.UtcNow;
}