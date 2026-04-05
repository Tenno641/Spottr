using GymManagement.Domain.GymAggregate;

namespace GymManagement.Application.Common.Interface;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
    Task<Gym?> GetGymByIdAsync(Guid id);
    Task<List<Gym>> ListGymsBySubscriptionIdAsync(Guid subscriptionId);
    Task UpdateGymAsync(Gym gym);
}