namespace SessionReservation.Contracts.Sessions;

public record SessionResponse(Guid RoomId, Guid TrainerId, string SessionType, int MinimumAge);