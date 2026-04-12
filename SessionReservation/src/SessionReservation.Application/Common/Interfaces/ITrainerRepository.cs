using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface ITrainerRepository
{
    Task<Trainer?> GetTrainerByIdAsync(Guid trainerId);
    Task UpdateTrainerAsync(Trainer trainer);
}