using ErrorOr;
using GymManagement.Application.Rooms.CreateRoom;
using GymManagement.Application.Rooms.DeleteRoom;
using GymManagement.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("gyms/{gymId:guid}/rooms")]
public class RoomsController: ApiController
{
    private readonly ISender _mediator;
    
    public RoomsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom(Guid gymId, CreateRoomRequest request)
    {
        CreateRoomCommand command = new CreateRoomCommand(gymId, request.Capacity);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : Ok();
    }

    [HttpDelete("{roomId:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid gymId, Guid roomId)
    {
        DeleteRoomCommand command = new DeleteRoomCommand(gymId, roomId);

        ErrorOr<Deleted> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : NoContent();
    }
}