using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.SessionAggregate.Events;

public record SessionCancelledEvent(): IDomainEvent;