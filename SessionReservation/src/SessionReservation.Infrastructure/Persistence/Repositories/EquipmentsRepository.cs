using Microsoft.EntityFrameworkCore;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Equipments;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class EquipmentsRepository: IEquipmentsRepository
{
    private readonly SessionReservationDbContext _dbContext;
    
    public EquipmentsRepository(SessionReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Equipment>> GetEquipmentsById(List<Guid> ids)
    {
        return await _dbContext.Equipments.Where(equipment => ids.Contains(equipment.Id)).ToListAsync();
    }
    
    public async Task<Equipment?> GetEquipmentById(Guid id)
    {
        return await _dbContext.Equipments.FirstOrDefaultAsync(equipment => equipment.Id == id);
    }
}