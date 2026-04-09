using ErrorOr;

namespace SessionReservation.Domain.TrainerAggregate;

public static class TrainerErrors
{
    public static Error AlreadyTeachesThisSession = Error.Conflict("Trainer.TeachSession", "Trainer already teaches this session");
    public static Error CannotTeachOverlappingSessions = Error.Conflict("Trainer.TeachSession", "Trainer cannot teach overlapping sessions");
    public static Error SessionNotFound = Error.NotFound("Trainer.RemoveTrainer", "Trainer is  not found");
}