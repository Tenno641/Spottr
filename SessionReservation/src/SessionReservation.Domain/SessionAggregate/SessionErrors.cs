using ErrorOr;

namespace SessionReservation.Domain.SessionAggregate;

public static class SessionErrors
{
    public static Error SessionAlreadyReserved => Error.Conflict(code: "Session.ReserveSpot", description: "Session Already Reserved");
}