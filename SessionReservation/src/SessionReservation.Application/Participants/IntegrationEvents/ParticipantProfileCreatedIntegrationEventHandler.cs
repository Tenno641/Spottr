using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SharedKernel.UserManagement;

namespace SessionReservation.Application.Participants.IntegrationEvents;

public class ParticipantProfileCreatedIntegrationEventHandler: INotificationHandler<ParticipantProfileCreatedIntegrationEvent>
{
    private readonly IParticipantRepository _participantRepository;
    
    public ParticipantProfileCreatedIntegrationEventHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task Handle(ParticipantProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Participant participant = new Participant(id: notification.ParticipantId, userId: notification.UserId, name: notification.Name, age: notification.Age);

        await _participantRepository.AddParticipantAsync(participant);
    }
}