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
    
    public async Task<List<Equipment>> GetEquipmentsByIds(Guid gymId, List<Guid> ids)
    {
        return await _context.Gyms
            .Where(gym => gym.Id == gymId)
            .SelectMany(gym => gym.Equipments)
            .ToListAsync();
    }
}