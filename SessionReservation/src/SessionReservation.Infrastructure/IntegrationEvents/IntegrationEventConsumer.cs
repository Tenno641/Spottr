using MassTransit;
using MediatR;
using SharedKernel.GymManagement;

namespace SessionReservation.Infrastructure.IntegrationEvents;

public class IntegrationEventConsumer: 
    IConsumer<RoomAddedIntegrationEvent>,
    IConsumer<RoomDeletedIntegrationEvent>
{
    private readonly IPublisher _publisher;
    
    public IntegrationEventConsumer(IPublisher publisher)
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
}