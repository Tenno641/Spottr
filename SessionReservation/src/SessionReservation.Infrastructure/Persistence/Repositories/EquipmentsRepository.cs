using Microsoft.EntityFrameworkCore;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.Entities;

namespace SessionReservation.Infrastructure.Persistence.Repositories;

public class EquipmentsRepository: IEquipmentsRepository
{
    private readonly SessionReservationDbContext _context;
    
    public EquipmentsRepository(SessionReservationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Equipment>> GetEquipmentsByIds(List<Guid> ids)
    {
        return await _context.Equipments
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();
    }
    
    public async Task UpdateEquipmentsAsync(List<Equipment> equipments)
    {
        _context.Equipments.UpdateRange(equipments);
        
        await _context.SaveChangesAsync();
    }
}