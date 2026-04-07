namespace SharedKernel.GymManagement;

public record RoomDeletedIntegrationEvent(Guid RoomId): IIntegrationEvent;