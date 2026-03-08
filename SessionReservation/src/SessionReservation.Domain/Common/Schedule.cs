using ErrorOr;

namespace SessionReservation.Domain.Common;

public class Schedule: Entity
{
    private Dictionary<DateOnly, List<TimeRange>> _calendar;

    public Schedule(Dictionary<DateOnly, List<TimeRange>>? calendar = null,
        Guid? id = null) : base(id)
    {
        _calendar = calendar ?? [];
    }


    public ErrorOr<Created> BookTimeSlot(DateOnly date, TimeRange timeRange)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots))
        {
            _calendar[date] = [timeRange];
            return Result.Created;
        }
        
        if (IsTimeSlotOccupied(date, timeRange))
            return Error.Conflict();
        
        timeSlots.Add(timeRange);
        return Result.Created;
    }

    public ErrorOr<Deleted> RemoveBooking(DateOnly date, TimeRange timeRange)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots) || !timeSlots.Contains(timeRange))
            return Error.NotFound();

        timeSlots.Remove(timeRange);
        
        return Result.Deleted;
    }

    public bool IsTimeSlotOccupied(DateOnly date, TimeRange timeRange)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots))
            return false;

        return timeSlots.Any(slot => slot.OverlapsWith(timeRange));
    }
}