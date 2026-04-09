using MediatR;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.RoomAggregate.Events;

public record SessionScheduledEvent(Session Session): IDomainEvent;