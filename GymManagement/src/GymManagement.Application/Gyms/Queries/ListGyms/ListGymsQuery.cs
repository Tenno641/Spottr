using GymManagement.Domain.GymAggregate;
using MediatR;
using ErrorOr;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId): IRequest<ErrorOr<List<Gym>>>;