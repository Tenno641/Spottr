using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(Guid SubscriptionId, string Name, int MaxRooms): IRequest<ErrorOr<Guid>>;