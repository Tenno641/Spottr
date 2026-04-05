using ErrorOr;
using MediatR;

namespace GymManagement.Application.Rooms.CreateRoom;

public class CreateRoomCommand(Guid GymId, int MaxDailySessions, int Capacity): IRequest<ErrorOr<Guid>>;