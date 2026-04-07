using GymManagement.Application.Common.Interface;
using GymManagement.Domain.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Persistence.Repositories;

public class SubscriptionsRepository: ISubscriptionsRepository
{
    private readonly GymManagementDbContext _dbContext;
    
    public SubscriptionsRepository(GymManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(Guid id)
    {
        return await _dbContext.Subscriptions
            .FirstOrDefaultAsync(subscription => subscription.Id == id);
    }
    
    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        _dbContext.Subscriptions.Add(subscription);
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Subscription subscription)
    {
        _dbContext.Subscriptions.Update(subscription);
        
        await _dbContext.SaveChangesAsync();
    }
}