using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Commands.CreateSession;

public class CreateSessionCommandHandler: IRequestHandler<CreateSessionCommand, ErrorOr<Guid>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly IEquipmentsRepository _equipmentsRepository;
    
    public CreateSessionCommandHandler(
        IRoomsRepository roomsRepository, IEquipmentsRepository equipmentsRepository)
    {
        _roomsRepository = roomsRepository;
        _equipmentsRepository = equipmentsRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetRoomByIdAsync(request.RoomId);
        if (room is null)
            return Error.NotFound("Room is not found");

        List<Equipment> equipments = await _equipmentsRepository.GetEquipmentsByIds(request.EquipmentsIds);
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
        
        await _roomsRepository.UpdateRoomAsync(room);

        return session.Id;
    }
}