using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate.Events;
using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Application.Trainers.Events;

public class SessionCancelledEventHandler: INotificationHandler<SessionCancelledEvent> 
{
    private readonly ITrainerRepository  _trainerRepository;
    
    public SessionCancelledEventHandler(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public async Task Handle(SessionCancelledEvent notification, CancellationToken cancellationToken)
    {
        Trainer? trainer = await _trainerRepository.GetTrainerByIdAsync(notification.Session.TrainerId);
        
        if (trainer is null)
            return;
        // TODO: Eventual Consistency Exception

        trainer.RemoveTrainer(notification.Session);
        
        await _trainerRepository.UpdateTrainerAsync(trainer);
    }
}