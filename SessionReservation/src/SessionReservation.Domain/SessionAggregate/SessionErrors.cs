using ErrorOr;

namespace SessionReservation.Domain.SessionAggregate;

public static class SessionErrors
{
    public static Error SessionAlreadyReserved => Error.Conflict(code: "Session.ReserveSpot", description: "Session Already Reserved");
    public static Error ParticipantMustMeetTheMinimumAge => Error.Forbidden(code: "Session.ReserveSpot", description: "Participant must meet the minimum age");

}