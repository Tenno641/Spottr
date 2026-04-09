using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;
using SharedKernel.GymManagement;

namespace SessionReservation.Application.Rooms.IntegrationEvents;

public class RoomAddedIntegrationEventHandler 
    : INotificationHandler<RoomAddedIntegrationEvent>
{
    private readonly IRoomRepository _roomRepository;
    
    public RoomAddedIntegrationEventHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task Handle(RoomAddedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Room room = new Room(
            capacity: notification.Capacity,
            maxDailySessions: notification.MaxDailySessions,
            gymId: notification.GymId,
            id: notification.RoomId);

        await _roomRepository.AddRoomAsync(room);
    }
}