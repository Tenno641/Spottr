using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate.Events;

namespace SessionReservation.Application.Sessions.Events;

public class SessionCancelledEventHandler: INotificationHandler<SessionCancelledEvent>
{
    private readonly ISessionsRepository _sessionsRepository;
    
    public SessionCancelledEventHandler(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(SessionCancelledEvent notification, CancellationToken cancellationToken)
    {
        await _sessionsRepository.DeleteSessionAsync(notification.Session);
    }
}