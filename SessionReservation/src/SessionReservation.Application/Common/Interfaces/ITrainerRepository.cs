using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface ITrainerRepository
{
    Task AddTrainerAsync(Trainer trainer);
    Task<Trainer?> GetTrainerByIdAsync(Guid trainerId);
    Task UpdateTrainerAsync(Trainer trainer);
}