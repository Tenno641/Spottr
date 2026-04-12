using Microsoft.EntityFrameworkCore;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class RoomsesRepository: IRoomsRepository
{
    private readonly SessionReservationDbContext _dbContext;
    
    public RoomsesRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Room?> GetRoomByIdAsync(Guid id)
    {
        return await _dbContext.Rooms.FirstOrDefaultAsync(room => room.Id == id);
    }
    
    public async Task AddRoomAsync(Room room)
    {
        _dbContext.Rooms.Add(room);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRoomByIdAsync(Room room)
    {
        _dbContext.Rooms.Remove(room);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Room>> ListRoomsByGymIdAsync(Guid gymId)
    {
        return await _dbContext.Rooms.Where(room => room.GymId == gymId).ToListAsync();
    }
    
    public async Task UpdateRoomAsync(Room room)
    {
        _dbContext.Rooms.Update(room);
        
        await _dbContext.SaveChangesAsync();
    }
}