using MassTransit;
using MediatR;
using SharedKernel.UserManagement;

namespace GymManagement.Infrastructure.IntegrationEvents;

public class IntegrationEventsConsumer: IConsumer<AdminProfileCreatedIntegrationEvent>
{
    private readonly IPublisher _publisher;
    
    public IntegrationEventsConsumer(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Consume(ConsumeContext<AdminProfileCreatedIntegrationEvent> context)
    {
        await _publisher.Publish(context.Message);
    }
}