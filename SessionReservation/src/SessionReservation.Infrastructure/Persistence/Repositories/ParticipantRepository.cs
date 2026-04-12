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

    public async Task AddParticipantAsync(Participant participant)
    {
        _dbContext.Participants.Add(participant);
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Participant?> GetByIdAsync(Guid participantId)
    {
        Participant? participant = await _dbContext.Participants
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == participantId);
        
        return participant;
    }
    
    public Task UpdateParticipantAsync(Participant participant)
    {
        _dbContext.Update(participant);
        return Task.CompletedTask;
    }
    
    public async Task<List<Participant>> GetParticipantsBySessionAsync(Guid sessionId)
    {
        return await _dbContext.Participants.Where(p => p.SessionIds.Contains(sessionId)).ToListAsync();
    }
    
    public async Task UpdateParticipantsAsync(List<Participant> participants)
    {
        _dbContext.Participants.UpdateRange(participants);
        
        await _dbContext.SaveChangesAsync();
    }
}