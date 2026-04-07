using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.AdminAggregate;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands;

public class CreateSubscriptionCommandHandler: IRequestHandler<CreateSubscriptionCommand, ErrorOr<Guid>>
{
    private readonly IAdminsRepository _adminsRepository;
    
    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository, IAdminsRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        Admin? admin = await _adminsRepository.GetByIdAsync(request.AdminId);
        if (admin is null)
            return Error.NotFound(description: "Admin is not found");

        Subscription subscription = new Subscription(request.AdminId, request.SubscriptionType);

        ErrorOr<Success> result = admin.SetSubscription(subscription);
        
        if (result.IsError)
            return result.Errors;
        
        await _adminsRepository.UpdateAsync(admin);
        
        return subscription.Id;
    }
}