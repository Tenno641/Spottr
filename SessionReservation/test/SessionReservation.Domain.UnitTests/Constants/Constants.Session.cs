using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.Equipments;

namespace SessionReservation.Domain.UnitTests.Constants;

public partial class Constants
{
    public static class Sessions
    {
        public static Guid Id => Guid.CreateVersion7();
        public static Guid RoomId => Guid.CreateVersion7();
        public static int Capacity => 20;
        public static DateOnly Date => DateOnly.MinValue;
        public static int MinimumAge => int.MaxValue;
        public static TimeRange TimeRange => new TimeRange(
            start: Constants.TimeRanges.Start,
            end: Constants.TimeRanges.End);
        public static List<Equipment> RequiredEquipments => [];
    }
}