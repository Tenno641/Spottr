using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.SessionAggregate.Events;

public record ReservationCancelledEvent(Session Session, Guid ParticipantId) : IDomainEvent;