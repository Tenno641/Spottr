namespace SharedKernel.GymManagement;

public record TrainerAddedIntegrationEvent(Guid GymId, Guid TrainerId): IIntegrationEvent;