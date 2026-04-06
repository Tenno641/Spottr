using ErrorOr;
using GymManagement.Domain.SubscriptionAggregate;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands;

public record CreateSubscriptionCommand(Guid AdminId, SubscriptionType SubscriptionType): IRequest<ErrorOr<Guid>>;