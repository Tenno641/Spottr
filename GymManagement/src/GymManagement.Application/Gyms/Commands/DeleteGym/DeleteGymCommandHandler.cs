using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler: IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    
    public DeleteGymCommandHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
    {
        Subscription? subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(request.SubscriptionId);
        if (subscription is null)
            return Error.NotFound("Subscription not found");
        
        ErrorOr<Deleted> result = subscription.RemoveGym(request.GymId);

        if (result.IsError)
            return result.Errors;
        
        await _subscriptionsRepository.UpdateAsync(subscription);

        return Result.Deleted;
    }
}