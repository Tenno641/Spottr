using GymManagement.Domain.Common;
using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.GymAggregate.Events;

public record RoomAddedEvent(Room Room): IDomainEvent;