using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class SessionsRepository: ISessionsRepository
{
    private readonly SessionReservationDbContext _dbContext;
    
    public SessionsRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddSessionAsync(Session session)
    {
        _dbContext.Sessions.Add(session);
        await _dbContext.SaveChangesAsync();
    }
}