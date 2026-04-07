using GymManagement.Domain.Common;
using GymManagement.Domain.GymAggregate;

namespace GymManagement.Domain.SubscriptionAggregate.Events;

public record GymDeletedEvent(Guid GymId): IDomainEvent;