namespace SharedKernel.UserManagement;

public record ParticipantProfileCreatedIntegrationEvent(Guid UserId, Guid ParticipantId, string Name, int Age): IIntegrationEvent;