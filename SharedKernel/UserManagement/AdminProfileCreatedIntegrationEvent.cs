namespace SharedKernel.UserManagement;

public record AdminProfileCreatedIntegrationEvent(Guid UserId, Guid AdminId): IIntegrationEvent;