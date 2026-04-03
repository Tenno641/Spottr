namespace SessionReservation.Contracts.Sessions;

public record CreateSessionRequest(
    Guid TrainerId,
    int? Capacity,
    string SessionType,
    DateOnly Date,
    TimeOnly Start,
    TimeOnly End,
    List<Guid> EquipmentsIds,
    int MinimumAge);