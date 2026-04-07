using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate.Events;
using MediatR;

namespace GymManagement.Application.Gyms.Events;

public class GymDeletedEventHandler: INotificationHandler<GymDeletedEvent>
{
    private readonly IGymsRepository _gymRepository;
    
    public GymDeletedEventHandler(IGymsRepository gymRepository)
    {
        _gymRepository = gymRepository;
    }
    
    public async Task Handle(GymDeletedEvent notification, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymRepository.GetGymByIdAsync(notification.GymId);
        
        if (gym is null)
            return;
        
        await _gymRepository.DeleteGymAsync(gym);
    }
}