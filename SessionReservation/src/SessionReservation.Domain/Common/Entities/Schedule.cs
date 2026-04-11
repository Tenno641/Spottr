using ErrorOr;
using SessionReservation.Domain.Common.ValueObjects;

namespace SessionReservation.Domain.Common.Entities;

public class Schedule: Entity
{
    public Dictionary<DateOnly, List<TimeRange>> Calendar { get; }

    public Schedule(Dictionary<DateOnly, List<TimeRange>>? calendar = null,
        Guid? id = null) : base(id)
    {
        Calendar = calendar ?? [];
    }


    public void BookTimeSlot(DateOnly date, TimeRange timeRange)
    {
        if (!Calendar.TryGetValue(date, out List<TimeRange>? timeSlots))
        {
            timeSlots = [];
            Calendar[date] = timeSlots;
        }
        
        timeSlots.Add(timeRange);
    }

    public ErrorOr<Deleted> RemoveBooking(DateOnly date, TimeRange timeRange)
    {
        if (!Calendar.TryGetValue(date, out List<TimeRange>? timeSlots) || !timeSlots.Contains(timeRange))
            return Error.NotFound();

        timeSlots.Remove(timeRange);
        
        return Result.Deleted;
    }

    public bool IsTimeSlotOccupied(DateOnly date, TimeRange timeRange)
    {
        if (!Calendar.TryGetValue(date, out List<TimeRange>? timeSlots))
            return false;

        return timeSlots.Any(slot => slot.OverlapsWith(timeRange));
    }
}