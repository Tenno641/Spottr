using SessionReservation.Domain.ParticipantAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface IParticipantRepository
{
    Task<Participant?> GetByIdAsync(Guid participantId);
    Task UpdateParticipant(Participant participant);
}