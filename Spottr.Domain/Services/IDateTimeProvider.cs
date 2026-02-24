namespace Spottr.Domain.Services;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}