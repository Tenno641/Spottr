using GymManagement.Domain.Common;

namespace GymManagement.Domain.SubscriptionAggregate.Events;

public record GymDeletedEvent(Guid GymId): IDomainEvent;