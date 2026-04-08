using ErrorOr;
using MediatR;

namespace GymManagement.Application.Rooms.CreateRoom;

public record CreateRoomCommand(Guid GymId, int Capacity): IRequest<ErrorOr<Guid>>;