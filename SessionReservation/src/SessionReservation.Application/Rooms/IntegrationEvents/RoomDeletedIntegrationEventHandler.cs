using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;
using SharedKernel.GymManagement;

namespace SessionReservation.Application.Rooms.IntegrationEvents;

public class RoomDeletedIntegrationEventHandler: INotificationHandler<RoomDeletedIntegrationEvent>
{
    private readonly IRoomRepository _roomRepository;
    
    public RoomDeletedIntegrationEventHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task Handle(RoomDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Room? room = await _roomRepository.GetRoomByIdAsync(notification.RoomId);
        if (room is null)
            return;
        
        await _roomRepository.DeleteRoomByIdAsync(room);
    }
}