using GymManagement.Application.Common.Interface;
using GymManagement.Domain.Common.Entities;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class EquipmentsRepository: IEquipmentRepository
{
    private readonly GymManagementDbContext _context;
    
    public EquipmentsRepository(GymManagementDbContext context)
    {
        _context = context;
    }

    public async Task AddEquipmentsAsync(List<Equipment> equipments)
    {
        _context.Equipments.AddRange(equipments);
        
        await _context.SaveChangesAsync();
    }
}