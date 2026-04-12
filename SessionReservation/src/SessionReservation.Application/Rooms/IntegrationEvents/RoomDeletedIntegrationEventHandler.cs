using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;
using SharedKernel.GymManagement;

namespace SessionReservation.Application.Rooms.IntegrationEvents;

public class RoomDeletedIntegrationEventHandler: INotificationHandler<RoomDeletedIntegrationEvent>
{
    private readonly IRoomsRepository _roomsRepository;
    
    public RoomDeletedIntegrationEventHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetRoomByIdAsync(notification.RoomId);
        if (room is null)
            return;
        
        await _roomsRepository.DeleteRoomByIdAsync(room);
    }
}