using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.Interfaces;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Commands.CancelReservation;

public class CancelReservationCommandHandler: IRequestHandler<CancelReservationCommand, ErrorOr<Success>>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public CancelReservationCommandHandler(ISessionsRepository sessionsRepository, IDateTimeProvider dateTimeProvider)
    {
        _sessionsRepository = sessionsRepository;
        _dateTimeProvider = dateTimeProvider;
    }
    
    public async Task<ErrorOr<Success>> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        Session? session = await _sessionsRepository.GetSessionByIdAsync(request.SessionId);

        if (session is null)
            return Error.NotFound(description: "Session is not found");

        session.CancelReservation(request.ParticipantId, _dateTimeProvider);
        
        await _sessionsRepository.UpdateSessionAsync(session);

        return Result.Success;
    }
}