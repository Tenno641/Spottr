using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionReservation.Application.Participants.Queries.ListParticipantSessions;
using SessionReservation.Application.Sessions.Commands.CancelReservation;
using SessionReservation.Application.Sessions.Commands.CreateReservation;
using SessionReservation.Contracts.Sessions;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Api.Controller;

[Route("participants")]
public class ParticipantsController : ApiController
{
    private readonly ISender _sender;

    public ParticipantsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{participantId:guid}/sessions")]
    public async Task<IActionResult> ListParticipantSessions(
        Guid participantId,
        int pageIndex = 1,
        DateTime? startDateTime = null,
        DateTime? endDateTime = null,
        List<SessionTypes>? types = null)
    {
        var query = new ListParticipantSessionsQuery(
            participantId,
            startDateTime,
            endDateTime,
            types);

        var listParticipantSessionsResult = await _sender.Send(query);
        
        return listParticipantSessionsResult.Match(sessions =>
            {
                List<SessionResponse> sessionResponses = sessions.ConvertAll(session => new SessionResponse(
                    RoomId: session.RoomId,
                    TrainerId: session.TrainerId,
                    SessionType: session.Type.ToString(),
                    MinimumAge: session.MinimumAge));
                
                Pagination<SessionResponse> paginatedResponse = Pagination<SessionResponse>.Create(sessionResponses, pageIndex);
            
            return Ok(paginatedResponse);
        },
        Problem);
    }

    [HttpDelete("{participantId:guid}/sessions/{sessionId:guid}/reservation")]
    public async Task<IActionResult> CancelReservation(
        Guid participantId,
        Guid sessionId)
    {
        var command = new CancelReservationCommand(participantId, sessionId);

        var cancelReservationResult = await _sender.Send(command);

        return cancelReservationResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPost("{participantId:guid}/sessions/{sessionId:guid}/reservation")]
    public async Task<IActionResult> CreateReservation(
        Guid participantId,
        Guid sessionId)
    {
        var command = new CreateReservationCommand(participantId, sessionId);

        var cancelReservationResult = await _sender.Send(command);

        return cancelReservationResult.Match(
            _ => NoContent(),
            Problem);
    }
}