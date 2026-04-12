using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;
using SharedKernel.GymManagement;

namespace SessionReservation.Application.Rooms.IntegrationEvents;

public class RoomDeletedIntegrationEventHandler: INotificationHandler<RoomDeletedIntegrationEvent>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ISessionsRepository _sessionsRepository;
    
    public RoomDeletedIntegrationEventHandler(IRoomsRepository roomsRepository, ISessionsRepository sessionsRepository)
    {
        _roomsRepository = roomsRepository;
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(RoomDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetRoomByIdAsync(notification.RoomId);

        if (room is not null)
        {
            List<Session> sessions = await _sessionsRepository.ListBySessionsIdsAsync(room.SessionIds);

            foreach (Session session in sessions)
                room.CancelSession(session);
            
            await _roomsRepository.DeleteRoomByIdAsync(room);
        }
    }
}