using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionReservation.Contracts.Sessions;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;
using ErrorOr;
using SessionReservation.Application.Sessions.Commands.CreateSession;

namespace SessionReservation.Api.Controller;

[Controller]
public class RoomsController: ApiController
{
    private readonly ISender _mediatr;
    
    public RoomsController(ISender mediatr)
    {
        _mediatr = mediatr;
    }
    
    [HttpPost("{roomId:guid}")]
    public async Task<IActionResult> CreateSession(Guid roomId, CreateSessionRequest request)
    {
        if (!Enum.TryParse(request.SessionType, out SessionTypes type))
        {
            return BadRequest("Invalid session type");
        }

        TimeRange timeRange = new TimeRange(request.Start, request.End);

        CreateSessionCommand command = new CreateSessionCommand(
            RoomId: roomId,
            TrainerId: request.TrainerId,
            Capacity: request.Capacity,
            SessionType: type,
            Date: request.Date,
            TimeRange: timeRange,
            EquipmentsIds: request.EquipmentsIds,
            MinimumAge: request.MinimumAge);

        ErrorOr<Guid> result = await _mediatr.Send(command);

        return result.IsError
            ? Problem(result.Errors)
            : Ok(result.Value);
    }
}