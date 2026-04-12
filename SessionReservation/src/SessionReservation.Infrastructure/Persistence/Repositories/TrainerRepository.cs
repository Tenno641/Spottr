using Microsoft.EntityFrameworkCore;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class TrainerRepository: ITrainerRepository
{
    private readonly SessionReservationDbContext _dbContext;
    
    public TrainerRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Trainer?> GetTrainerByIdAsync(Guid trainerId)
    {
        return await _dbContext.Trainers.FirstOrDefaultAsync(s => s.Id == trainerId);
    }
    
    public async Task UpdateTrainerAsync(Trainer trainer)
    {
        _dbContext.Trainers.Update(trainer);
        
        await _dbContext.SaveChangesAsync();
    }
}