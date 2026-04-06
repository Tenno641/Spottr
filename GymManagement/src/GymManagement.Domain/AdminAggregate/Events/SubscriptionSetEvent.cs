using GymManagement.Domain.Common;
using GymManagement.Domain.SubscriptionAggregate;

namespace GymManagement.Domain.AdminAggregate.Events;

public record SubscriptionSetEvent(Subscription Subscription): IDomainEvent;