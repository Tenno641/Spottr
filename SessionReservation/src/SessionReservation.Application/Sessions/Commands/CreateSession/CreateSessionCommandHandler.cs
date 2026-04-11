using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Commands.CreateSession;

public class CreateSessionCommandHandler: IRequestHandler<CreateSessionCommand, ErrorOr<Guid>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IEquipmentsRepository _equipmentsRepository;
    
    public CreateSessionCommandHandler(
        IRoomRepository roomRepository, ISessionsRepository sessionsRepository, IEquipmentsRepository equipmentsRepository)
    {
        _roomRepository = roomRepository;
        _sessionsRepository = sessionsRepository;
        _equipmentsRepository = equipmentsRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        Room? room = await _roomRepository.GetRoomByIdAsync(request.RoomId);
        if (room is null)
            return Error.NotFound("Room is not found");

        List<Equipment> equipments = await _equipmentsRepository.GetEquipmentsByIds(room.GymId, request.EquipmentsIds);
        if (equipments.Count != request.EquipmentsIds.Count)
            return Error.NotFound("Required equipments are not found");
        
        Session session = new Session(
            trainerId: request.TrainerId,
            roomId: request.RoomId,
            capacity: request.Capacity,
            type: request.SessionType,
            date: request.Date,
            timeRange: request.TimeRange,
            minimumAge: request.MinimumAge,
            equipments: equipments);
        
        ErrorOr<Created> result = room.ScheduleSession(session);

        if (result.IsError)
            return result.Errors;

        await _sessionsRepository.AddSessionAsync(session); // TODO: Remove this and let the room create the session
        
        // TODO: Equipment must be an Entity and doesn't have DbSet in EF

        return session.Id;
    }
}