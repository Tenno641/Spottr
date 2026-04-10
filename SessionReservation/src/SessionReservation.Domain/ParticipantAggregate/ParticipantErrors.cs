using ErrorOr;

namespace SessionReservation.Domain.ParticipantAggregate;

public static class ParticipantErrors
{
    public static Error ParticipantHasOverlappingSessions =>
        Error.Conflict(code: "Session.ReserveSpot", description: "Participant already has session reserved conflicting this one"); 
    
    public static Error AlreadyReservedThisSession =>
        Error.Conflict(code: "Session.ReserveSpot", description: "Participant already has current session reserved"); 
    
    public static Error SessionIsNotFound => 
        Error.NotFound(code: "Participant.CancelSession", description: "Participant is not found");
}