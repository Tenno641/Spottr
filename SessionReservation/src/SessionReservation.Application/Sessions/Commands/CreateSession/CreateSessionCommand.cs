using ErrorOr;
using MediatR;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Commands.CreateSession;

public record CreateSessionCommand(
    Guid RoomId,
    Guid TrainerId,
    int? Capacity,
    SessionTypes SessionType,
    DateOnly Date,
    TimeRange TimeRange,
    List<Guid> EquipmentsIds,
    int MinimumAge): IRequest<ErrorOr<Guid>>;
