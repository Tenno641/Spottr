using SessionReservation.Domain.Common;

namespace SessionReservation.Domain.SessionAggregate.Events;

public record SessionSpotReservedEvent(Session Session, Guid ParticipantId) : IDomainEvent;