using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Queries.ListSessions;

public class ListSessionsQueryHandler: IRequestHandler<ListSessionsQuery, ErrorOr<List<Session>>>
{
    private readonly  ISessionsRepository _sessionsRepository;
    
    public ListSessionsQueryHandler(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }
    
    public async Task<ErrorOr<List<Session>>> Handle(ListSessionsQuery request, CancellationToken cancellationToken)
    {
        return await _sessionsRepository.ListByGymIdAsync(request.GymId, request.StartDateTime, request.EndDateTime, request.Types);
    }
}