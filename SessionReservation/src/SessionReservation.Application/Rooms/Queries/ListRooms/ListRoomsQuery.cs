using ErrorOr;
using MediatR;
using SessionReservation.Domain.RoomAggregate;

namespace SessionReservation.Application.Rooms.Queries.ListRooms;

public record ListRoomsQuery(Guid GymId): IRequest<ErrorOr<List<Room>>>;