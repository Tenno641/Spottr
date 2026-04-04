using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Application.Common.Interface;

public interface ISubscriptionsRepository
{
    Task<Subscription?> GetSubscriptionByIdAsync(Guid id);
}