using ErrorOr;
using MediatR;
using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Application.Rooms.Queries.GetRoom;

public record GetRoomQuery(Guid RoomId): IRequest<ErrorOr<Room>>;