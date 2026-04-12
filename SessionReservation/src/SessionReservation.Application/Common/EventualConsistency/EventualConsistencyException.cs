using ErrorOr;

namespace SessionReservation.Application.Common.EventualConsistency;

public class EventualConsistencyException: Exception
{
    public string Description { get; }
    public List<Error> Errors { get; }
    
    public EventualConsistencyException(string description, List<Error>? errors = null) : base(description)
    {
        Description = description;
        Errors = errors ?? [];
    }
}