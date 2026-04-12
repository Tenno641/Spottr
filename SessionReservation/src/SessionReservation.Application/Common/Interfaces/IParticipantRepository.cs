using SessionReservation.Domain.ParticipantAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface IParticipantRepository
{
    Task<Participant?> GetByIdAsync(Guid participantId);
    Task UpdateParticipantAsync(Participant participant);
    Task<List<Participant>> GetParticipantsBySessionAsync(Guid sessionId);
    Task UpdateParticipantsAsync(List<Participant> participants);
}