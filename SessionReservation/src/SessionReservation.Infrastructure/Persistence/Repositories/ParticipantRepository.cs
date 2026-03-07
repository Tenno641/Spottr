using Microsoft.EntityFrameworkCore;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class ParticipantRepository: IParticipantRepository
{
    private readonly SessionReservationDbContext _dbContext;
    
    public ParticipantRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Participant?> GetByIdAsync(Guid participantId)
    {
        Participant? participant = await _dbContext.Participants
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == participantId);
        
        return participant;
    }
    
    public Task UpdateParticipant(Participant participant)
    {
        _dbContext.Update(participant);
        return Task.CompletedTask;
    }
}