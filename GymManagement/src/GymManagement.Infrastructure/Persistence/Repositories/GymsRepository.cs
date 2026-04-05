using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class GymsRepository: IGymsRepository
{
    private readonly GymManagementDbContext _dbContext;
    
    public GymsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddGymAsync(Gym gym)
    {
        _dbContext.Gyms.Add(gym);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Gym?> GetGymByIdAsync(Guid id)
    {
        return await _dbContext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
    }
    
    public async Task<List<Gym>> ListGymsBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await _dbContext.Gyms
            .AsNoTracking()
            .Where(gym => gym.SubscriptionId == subscriptionId)
            .ToListAsync();
    }
    
    public async Task UpdateGymAsync(Gym gym)
    {
        _dbContext.Gyms.Update(gym);
        await _dbContext.SaveChangesAsync();
    }
}