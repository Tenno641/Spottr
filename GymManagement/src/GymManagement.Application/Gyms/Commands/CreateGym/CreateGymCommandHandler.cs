using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler: IRequestHandler<CreateGymCommand, ErrorOr<Guid>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    
    public CreateGymCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        Subscription? subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(request.SubscriptionId);

        if (subscription is null)
            return Error.NotFound();

        Gym gym = new Gym(
            subscriptionId: request.SubscriptionId,
            maxRooms: request.MaxRooms,
            name: request.Name);
        
        ErrorOr<Created> result = subscription.AddGym(gym);

        if (result.IsError)
            return result.Errors;

        await _gymsRepository.AddGymAsync(gym);

        return gym.Id;
    }
}