using GymManagement.Domain.Common;
using GymManagement.Domain.Trainers;

namespace GymManagement.Domain.GymAggregate.Events;

public record TrainerAddedEvent(Trainer Trainer) : IDomainEvent;