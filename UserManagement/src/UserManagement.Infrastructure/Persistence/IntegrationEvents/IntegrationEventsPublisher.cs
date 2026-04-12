using MassTransit;
using MediatR;
using SharedKernel.UserManagement;
using UserManagement.Domain.UserAggregate.Events;

namespace UserManagement.Infrastructure.Persistence.IntegrationEvents;

public class IntegrationEventsPublisher:
    INotificationHandler<ParticipantProfileCreatedEvent>,
    INotificationHandler<TrainerProfileCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    
    public IntegrationEventsPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task Handle(ParticipantProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        ParticipantProfileCreatedIntegrationEvent integrationEvent = new ParticipantProfileCreatedIntegrationEvent(notification.UserId, notification.ParticipantId, notification.Name, notification.Age);
        
        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
    
    public async Task Handle(TrainerProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        TrainerProfileCreatedIntegrationEvent integrationEvent =  new TrainerProfileCreatedIntegrationEvent(notification.UserId, notification.TrainerId, notification.Name);
        
        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}