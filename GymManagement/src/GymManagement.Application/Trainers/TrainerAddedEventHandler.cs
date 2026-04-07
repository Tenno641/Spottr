using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.GymAggregate.Events;
using MediatR;

namespace GymManagement.Application.Trainers;

public class TrainerAddedEventHandler: INotificationHandler<TrainerAddedEvent>
{
    private readonly IGymsRepository _gymsRepository;
    
    public TrainerAddedEventHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }
    
    public async Task Handle(TrainerAddedEvent notification, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetGymByIdAsync(notification.Trainer.GymId);

        if (gym is null)
            return;
        
        gym.AddTrainer(notification.Trainer);
        
        await _gymsRepository.UpdateGymAsync(gym);
    }
}