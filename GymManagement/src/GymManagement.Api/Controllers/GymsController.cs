using GymManagement.Application.Gyms.Commands;
using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Domain.GymAggregate;

namespace GymManagement.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController: ApiController
{
    private readonly ISender _mediator;
    
    public GymsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGym(Guid subscriptionId, CreateGymRequest request)
    {
        CreateGymCommand command = new CreateGymCommand(subscriptionId, request.Name, request.MaxRooms);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetGyms(Guid subscriptionId)
    {
        ListGymsQuery query = new ListGymsQuery(subscriptionId);

        ErrorOr<List<Gym>> result = await _mediator.Send(query);

        return result.IsError
            ? Problem(result.Errors)
            : Ok(result.Value);
    }
}