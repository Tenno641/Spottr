using ErrorOr;
using GymManagement.Application.Subscriptions.Commands;
using GymManagement.Contracts.Subscriptions;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("subscriptions")]
public class SubscriptionsController: ApiController
{
    private readonly ISender _mediator;
    
    public SubscriptionsController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if (!Enum.TryParse(request.SubscriptionType, out SubscriptionType subscriptionType))
        {
            return BadRequest();
        }
        
        CreateSubscriptionCommand command = new CreateSubscriptionCommand(request.AdminId, subscriptionType);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : Ok(result.Value); // TODO: Created
    }
    
    // TODO: Get Subscription using user ID : Required OAuth installed.
}