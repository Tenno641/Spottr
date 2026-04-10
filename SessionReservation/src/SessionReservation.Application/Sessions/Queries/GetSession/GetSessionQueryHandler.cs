using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Queries.GetSession;

public class GetSessionQueryHandler: IRequestHandler<GetSessionQuery, ErrorOr<Session>>
{
    private readonly ISessionsRepository _sessionsRepository;
    
    public GetSessionQueryHandler(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }
    
    public async Task<ErrorOr<Session>> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        Session? session = await _sessionsRepository.GetSessionByIdAsync(request.SessionId);

        if (session is null)
            return Error.NotFound(description: "Session is not found");

        return session;
    }
}