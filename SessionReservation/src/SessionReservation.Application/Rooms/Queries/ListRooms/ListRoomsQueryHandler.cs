using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Application.Rooms.Queries.ListRooms;

public class ListRoomsQueryHandler: IRequestHandler<ListRoomsQuery, ErrorOr<List<Room>>>
{
    private readonly IRoomRepository _roomRepository;
    
    public ListRoomsQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<ErrorOr<List<Room>>> Handle(ListRoomsQuery request, CancellationToken cancellationToken)
    {
        return await _roomRepository.ListRoomsByGymIdAsync(request.GymId);
    }
}