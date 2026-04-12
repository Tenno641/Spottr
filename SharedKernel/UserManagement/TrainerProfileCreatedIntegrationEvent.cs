namespace SharedKernel.UserManagement;

public record TrainerProfileCreatedIntegrationEvent(Guid UserId, Guid TrainerId, string Name): IIntegrationEvent;