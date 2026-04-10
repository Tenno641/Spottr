using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionReservation.Application.Sessions.Commands.CreateSession;
using SessionReservation.Contracts.Sessions;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;
using ErrorOr;
using SessionReservation.Application.Sessions.Queries.GetSession;

namespace SessionReservation.Api.Controller;

[Route("rooms/{roomId:guid}/sessions")]
public class SessionsController : ApiController
{
    private readonly ISender _sender;

    public SessionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession(
        CreateSessionRequest request,
        Guid roomId)
    {
        if (!Enum.TryParse(request.SessionType, out SessionTypes type))
            return BadRequest();

        TimeRange timeRange = new TimeRange(request.Start, request.End);

        var command = new CreateSessionCommand(
            RoomId: roomId,
            TrainerId: request.TrainerId,
            Capacity: request.Capacity,
            SessionType: type,
            Date: request.Date,
            TimeRange: timeRange,
            EquipmentsIds: request.EquipmentsIds,
            MinimumAge: request.MinimumAge);

        ErrorOr<Guid> createSessionResult = await _sender.Send(command);

        return createSessionResult.Match(sessionId => 
                CreatedAtAction(nameof(GetSession), new { roomId, SessionId = sessionId }, sessionId),
            Problem);
    }

    [HttpGet("{sessionId:guid}")]
    public async Task<IActionResult> GetSession(Guid sessionId)
    {
        var query = new GetSessionQuery(sessionId);

        var getSessionResult = await _sender.Send(query);

        return getSessionResult.Match(
            session => Ok(new SessionResponse(session.RoomId, session.TrainerId, session.Type.ToString(), session.MinimumAge)),
            Problem);
    }
}