using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Participants.Queries.ListParticipantSessions;

public record ListParticipantSessionsQueryHandler: IRequestHandler<ListParticipantSessionsQuery, ErrorOr<List<Session>>>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IParticipantRepository _participantRepository;
    
    public ListParticipantSessionsQueryHandler(ISessionsRepository sessionsRepository, IParticipantRepository participantRepository)
    {
        _sessionsRepository = sessionsRepository;
        _participantRepository = participantRepository;
    }
    
    public async Task<ErrorOr<List<Session>>> Handle(ListParticipantSessionsQuery request, CancellationToken cancellationToken)
    {
        Participant? participant = await _participantRepository.GetByIdAsync(request.ParticipantId);
        if (participant is null)
            return Error.NotFound(description: "Participant is not found");
        
        List<Session> sessions = await _sessionsRepository.ListBySessionsIdsAsync(participant.SessionIds, request.StartDateTime, request.EndDateTime, request.Types);

        return sessions;
    }
}