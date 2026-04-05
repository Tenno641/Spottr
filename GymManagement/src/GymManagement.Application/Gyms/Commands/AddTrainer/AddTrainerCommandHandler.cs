using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler: IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;
    
    public AddTrainerCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
    }
    
    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
    {
        Subscription? subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(request.SubscriptionId);
        if (subscription is null)
            return Error.NotFound(description: "Subscription is not found");

        Gym? gym = await _gymsRepository.GetGymById(request.GymId);
        
        if (gym is null)
            return Error.NotFound(description: "Gym is not found");

        ErrorOr<Success> result = gym.AddTrainer(request.TrainerId);
        
        return result.IsError
            ? result.Errors
            : result.Value;
    }
}