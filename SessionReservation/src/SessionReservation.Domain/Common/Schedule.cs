namespace SessionReservation.Domain.Common;

public class Schedule
{
    private Dictionary<DateOnly, List<TimeRange>> _timeSlots = [];

    public bool IsTimeSlotOccupied(DateOnly date, TimeRange timeRange)
    {
        
    }
}