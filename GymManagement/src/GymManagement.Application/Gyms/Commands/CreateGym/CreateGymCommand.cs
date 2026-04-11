using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(Guid SubscriptionId, string Name, List<string> Equipments): IRequest<ErrorOr<Guid>>;