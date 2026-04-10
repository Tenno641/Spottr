using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Application.Rooms.Queries.GetRoom;

public class GetRoomQueryHandler: IRequestHandler<GetRoomQuery, ErrorOr<Room>>
{
    private readonly IRoomRepository _roomRepository;
    
    public GetRoomQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }
    
    public async Task<ErrorOr<Room>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        Room? room = await _roomRepository.GetRoomByIdAsync(request.RoomId);

        if (room is null)
            return Error.NotFound(description: "Room is not found");

        return room;
    }
}