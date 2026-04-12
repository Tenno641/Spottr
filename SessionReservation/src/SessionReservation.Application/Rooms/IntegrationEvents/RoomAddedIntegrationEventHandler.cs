using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;
using SharedKernel.GymManagement;

namespace SessionReservation.Application.Rooms.IntegrationEvents;

public class RoomAddedIntegrationEventHandler 
    : INotificationHandler<RoomAddedIntegrationEvent>
{
    private readonly IRoomsRepository _roomsRepository;
    
    public RoomAddedIntegrationEventHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }
    
    public async Task Handle(RoomAddedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Room room = new Room(
            capacity: notification.Capacity,
            maxDailySessions: notification.MaxDailySessions,
            name: notification.Name,
            gymId: notification.GymId,
            id: notification.RoomId);

        await _roomsRepository.AddRoomAsync(room);
    }
}