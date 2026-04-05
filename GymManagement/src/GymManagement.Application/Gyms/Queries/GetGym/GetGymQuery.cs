using ErrorOr;
using GymManagement.Domain.GymAggregate;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.QueryGym;

public record GetGymQuery(Guid SubscriptionId, Guid GymId): IRequest<ErrorOr<Gym>>;