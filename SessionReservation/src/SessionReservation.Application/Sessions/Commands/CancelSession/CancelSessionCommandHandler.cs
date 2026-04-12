using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Commands.CancelSession;

public class CancelSessionCommandHandler: IRequestHandler<CancelSessionCommand, ErrorOr<Success>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ISessionsRepository _sessionsRepository;
    
    public CancelSessionCommandHandler(IRoomsRepository roomsRepository, ISessionsRepository sessionsRepository)
    {
        _roomsRepository = roomsRepository;
        _sessionsRepository = sessionsRepository;
    }
    
    public async Task<ErrorOr<Success>> Handle(CancelSessionCommand request, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetRoomByIdAsync(request.RoomId);
        if (room is null)
            return Error.NotFound(description: "Room is not found");
        
        Session? session = await _sessionsRepository.GetSessionByIdAsync(request.SessionId);
        if (session is null)
            return Error.NotFound(description: "Session is not found");

        ErrorOr<Success> result = room.CancelSession(session);

        if (result.IsError)
            return result.Errors;

        await _roomsRepository.UpdateRoomAsync(room);

        return Result.Success;
    }
}