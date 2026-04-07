using GymManagement.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using GymManagement.Application.Gyms.Commands.AddTrainer;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Application.Gyms.Commands.DeleteGym;
using GymManagement.Application.Gyms.Queries.ListGyms;
using GymManagement.Application.Gyms.Queries.QueryGym;
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
            : CreatedAtAction(nameof(GetGym), new { subscriptionId = subscriptionId, gymId = result.Value }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetGyms(Guid subscriptionId)
    {
        ListGymsQuery query = new ListGymsQuery(subscriptionId);

        ErrorOr<List<Gym>> result = await _mediator.Send(query);

        return result.IsError
            ? Problem(result.Errors)
            : Ok(result.Value.ConvertAll(gym => new ListGymsResponse(gym.Id, gym.Name)));
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
    {
        GetGymQuery command = new GetGymQuery(subscriptionId, gymId);

        ErrorOr<Gym> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : Ok(new GetGymResponse(result.Value.Id, result.Value.Name));
    }

    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> AddTrainer(Guid subscriptionId, Guid gymId, AddTrainerRequest request)
    {
        AddTrainerCommand command = new AddTrainerCommand(subscriptionId, gymId, request.TrainerId);

        ErrorOr<Success> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : Ok();
    }

    [HttpDelete("{gymId:guid}")]
    public async Task<IActionResult> DeleteGym(Guid subscriptionId, Guid gymId)
    {
        DeleteGymCommand command = new DeleteGymCommand(subscriptionId, gymId);

        ErrorOr<Deleted> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : Ok();
    }
}