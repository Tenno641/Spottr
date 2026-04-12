using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.RoomAggregate.Events;

public record SessionCancelledEvent(Session Session): IDomainEvent;