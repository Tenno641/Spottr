using GymManagement.Domain.GymAggregate;

namespace GymManagement.Application.Common.Interface;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
}