using Microsoft.EntityFrameworkCore;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;
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
    
    public async Task<List<Session>> ListBySessionsIdsAsync(IReadOnlyList<Guid> sessionsIds, DateTime? startDateTime = null, DateTime? endDateTime = null, List<SessionTypes>? types = null)
    {
        return await _dbContext.Sessions
            .AsNoTracking()
            .Where(session => sessionsIds.Contains(session.Id))
            .WhereSessionBetweenDateAndTime(startDateTime, endDateTime)
            .WhereSessionType(types)
            .ToListAsync();
    }
    
    public async Task<Session?> GetSessionByIdAsync(Guid sessionId)
    {
        return await _dbContext.Sessions.Where(session => session.Id == sessionId).FirstOrDefaultAsync();
    }
    
    public async Task UpdateSessionAsync(Session session)
    {
        _dbContext.Sessions.Update(session);
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Session>> ListByGymIdAsync(Guid gymId, DateTime? startDateTime = null, DateTime? endDateTime = null, List<SessionTypes>? types = null)
    {
        List<Room> rooms = await _dbContext.Rooms
            .AsNoTracking()
            .Where(room => room.GymId == gymId)
            .ToListAsync();

        List<Guid> sessionIds = rooms.SelectMany(room => room.SessionIds).ToList();
        
        return await ListBySessionsIdsAsync(sessionIds, startDateTime, endDateTime, types);
    }
}

static class SessionDbSetExtensions
{ 
    public static IQueryable<Session> WhereSessionBetweenDateAndTime(this IQueryable<Session> sessions, DateTime? startDateTime, DateTime? endDateTime)
    {
        if (startDateTime is null && endDateTime is null)
            return  sessions;
        
        startDateTime ??= DateTime.MinValue;
        endDateTime ??= DateTime.MaxValue;

        return sessions
            .Where(session => session.Date >= DateOnly.FromDateTime(startDateTime.Value) &&
                              session.TimeRange.Start >= TimeOnly.FromDateTime(startDateTime.Value))
            .Where(session => session.Date <= DateOnly.FromDateTime(endDateTime.Value) &&
                              session.TimeRange.Start <= TimeOnly.FromDateTime(endDateTime.Value));
    }

    public static IQueryable<Session> WhereSessionType(this IQueryable<Session> sessions, List<SessionTypes>? types)
    {
        if (types is null || types.Count == 0)
            return sessions;
        
        List<string> requiredTypes = types.ConvertAll(type => type.ToString());

        return sessions
            .Where(session => requiredTypes.Contains(session.Type.ToString()));
    }
}