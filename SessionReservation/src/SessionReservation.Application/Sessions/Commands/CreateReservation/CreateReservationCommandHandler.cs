using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Commands.CreateReservation;

public record CreateReservationCommandHandler: IRequestHandler<CreateReservationCommand, ErrorOr<Created>>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IParticipantRepository _participantRepository;
    
    public CreateReservationCommandHandler(ISessionsRepository sessionsRepository, IParticipantRepository participantRepository)
    {
        _sessionsRepository = sessionsRepository;
        _participantRepository = participantRepository;
    }
    
    public async Task<ErrorOr<Created>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        Session? session = await _sessionsRepository.GetSessionByIdAsync(request.SessionId);

        if (session is null)
            return Error.NotFound(description: "Session is not found");

        Participant? participant = await _participantRepository.GetByIdAsync(request.ParticipantId);

        if (participant is null)
            return Error.NotFound(description: "Participant is not found");
        
        ErrorOr<Created> reserveSpotResult = session.ReserveSpot(participant);
        
        if (reserveSpotResult.IsError)
            return  reserveSpotResult;

        await _sessionsRepository.UpdateSessionAsync(session);

        return Result.Created;
    }
}