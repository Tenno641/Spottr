using MassTransit;
using MediatR;
using SharedKernel.GymManagement;
using SharedKernel.UserManagement;

namespace SessionReservation.Infrastructure.IntegrationEvents;

public class IntegrationEventsConsumer: 
    IConsumer<RoomAddedIntegrationEvent>,
    IConsumer<RoomDeletedIntegrationEvent>,
    IConsumer<ParticipantProfileCreatedIntegrationEvent>,
    IConsumer<TrainerProfileCreatedIntegrationEvent>
{
    private readonly IPublisher _publisher;
    
    public IntegrationEventsConsumer(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public async Task Consume(ConsumeContext<RoomAddedIntegrationEvent> context)
    {
        await _publisher.Publish(context.Message);
    }
    
    public async Task Consume(ConsumeContext<RoomDeletedIntegrationEvent> context)
    {
        await _publisher.Publish(context.Message);
    }
    
    public async Task Consume(ConsumeContext<ParticipantProfileCreatedIntegrationEvent> context)
    {
        await _publisher.Publish(context.Message);
    }
    
    public async Task Consume(ConsumeContext<TrainerProfileCreatedIntegrationEvent> context)
    {
        await _publisher.Publish(context.Message);
    }
}