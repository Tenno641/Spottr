using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Common.Interfaces;

public interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
}