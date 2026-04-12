using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
    Task<List<Session>> ListBySessionsIdsAsync(IReadOnlyList<Guid> sessionsIds, DateTime? startDateTime = null, DateTime? endDateTime = null, List<SessionTypes>? types = null);
    Task<Session?> GetSessionByIdAsync(Guid sessionId);
    Task UpdateSessionAsync(Session session);
    Task<List<Session>> ListByGymIdAsync(Guid gymId, DateTime? startDateTime = null, DateTime? endDateTime = null, List<SessionTypes>? types = null);
    Task DeleteSessionAsync(Session session);
}