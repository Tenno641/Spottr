using Microsoft.EntityFrameworkCore;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class RoomsRepository: IRoomRepository
{
    private readonly SessionReservationDbContext _dbContext;
    
    public RoomsRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Room?> GetRoomByIdAsync(Guid id)
    {
        return await _dbContext.Rooms.FirstOrDefaultAsync(room => room.Id == id);
    }
}