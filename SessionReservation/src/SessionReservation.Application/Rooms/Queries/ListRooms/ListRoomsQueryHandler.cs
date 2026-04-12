using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Application.Rooms.Queries.ListRooms;

public class ListRoomsQueryHandler: IRequestHandler<ListRoomsQuery, ErrorOr<List<Room>>>
{
    private readonly IRoomsRepository _roomsRepository;
    
    public ListRoomsQueryHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }
    
    public async Task<ErrorOr<List<Room>>> Handle(ListRoomsQuery request, CancellationToken cancellationToken)
    {
        return await _roomsRepository.ListRoomsByGymIdAsync(request.GymId);
    }
}