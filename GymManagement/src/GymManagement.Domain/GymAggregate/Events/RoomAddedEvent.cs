using GymManagement.Domain.Common;
using MediatR;

namespace GymManagement.Domain.GymAggregate.Events;

public record RoomAddedEvent: IDomainEvent;