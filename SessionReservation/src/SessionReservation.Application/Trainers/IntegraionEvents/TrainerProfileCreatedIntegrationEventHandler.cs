using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.TrainerAggregate;
using SharedKernel.UserManagement;

namespace SessionReservation.Application.Trainers.IntegraionEvents;

public class TrainerProfileCreatedIntegrationEventHandler: INotificationHandler<TrainerProfileCreatedIntegrationEvent>
{
    private readonly ITrainerRepository _trainerRepository;
    
    public TrainerProfileCreatedIntegrationEventHandler(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public async Task Handle(TrainerProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Trainer trainer = new Trainer(userId: notification.UserId, id: notification.TrainerId, name: notification.Name);

        await _trainerRepository.AddTrainerAsync(trainer);
    }
}