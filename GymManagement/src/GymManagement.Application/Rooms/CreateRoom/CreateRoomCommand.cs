using ErrorOr;
using MediatR;

namespace GymManagement.Application.Rooms.CreateRoom;

public record CreateRoomCommand(Guid GymId, int MaxDailySessions, int Capacity): IRequest<ErrorOr<Guid>>;