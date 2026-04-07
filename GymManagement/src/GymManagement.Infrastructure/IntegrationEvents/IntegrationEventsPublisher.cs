using GymManagement.Domain.GymAggregate.Events;
using MassTransit;
using MediatR;
using SharedKernel.GymManagement;

namespace GymManagement.Infrastructure.IntegrationEvents;

public class IntegrationEventsPublisher: 
    INotificationHandler<RoomAddedEvent>,
    INotificationHandler<RoomDeletedEvent>,
    INotificationHandler<TrainerAddedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    
    public IntegrationEventsPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public Task Handle(RoomAddedEvent notification, CancellationToken cancellationToken)
    {
        RoomAddedIntegrationEvent integrationEvent = new RoomAddedIntegrationEvent(
            Capacity: notification.Room.Capacity,
            MaxDailySessions: notification.Room.MaxDailySessions,
            GymId: notification.Room.GymId,
            RoomId: notification.Room.Id);
        
        _publishEndpoint.Publish(integrationEvent, cancellationToken);
        
        return Task.CompletedTask;
    }
    
    public Task Handle(RoomDeletedEvent notification, CancellationToken cancellationToken)
    {
        RoomDeletedIntegrationEvent integrationEvent = new RoomDeletedIntegrationEvent(RoomId: notification.RoomId);
        
        _publishEndpoint.Publish(integrationEvent, cancellationToken);
        
        return Task.CompletedTask;
    }
    
    public Task Handle(TrainerAddedEvent notification, CancellationToken cancellationToken)
    {
        TrainerAddedIntegrationEvent integrationEvent = new TrainerAddedIntegrationEvent(
            GymId: notification.Trainer.GymId, 
            TrainerId: notification.Trainer.GymId);
        
        _publishEndpoint.Publish(integrationEvent, cancellationToken);
        
        return Task.CompletedTask;
    }
}