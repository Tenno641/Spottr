using ErrorOr;

namespace SessionReservation.Domain.ParticipantAggregate;

public static class ParticipantErrors
{
    public static Error ParticipantHasOverlappingSessions =>
        Error.Conflict(code: "Session.ReserveSpot", description: "Participant already has session reserved"); 
}