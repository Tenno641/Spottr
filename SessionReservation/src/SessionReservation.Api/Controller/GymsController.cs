using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionReservation.Api.Common;
using SessionReservation.Application.Sessions.Queries.ListSessions;
using SessionReservation.Contracts.Sessions;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Api.Controller;

[Route("gyms/{gymId:guid}")]
public class GymsController : ApiController
{
    private readonly ISender _sender;

    public GymsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("sessions")]
    public async Task<IActionResult> ListSessions(
        Guid gymId,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        List<string>? requiredType = null)
    {
        (bool isParsable, List<SessionTypes> sessionTypes) sessionsTypesResult = requiredType.ToSessionsTypes();

        if (!sessionsTypesResult.isParsable)
            return BadRequest();
        
        ListSessionsQuery command = new ListSessionsQuery(
            gymId,
            startDateTime,
            endDateTime,
            sessionsTypesResult.sessionTypes);

        var listSessionsResult = await _sender.Send(command);

        return listSessionsResult.Match(
            sessions => Ok(sessions.ConvertAll(session => new SessionResponse(
                session.RoomId, session.TrainerId, session.Type.ToString(), session.MinimumAge))),
            Problem);
    }
}