namespace SharedKernel.GymManagement;

public record RoomAddedIntegrationEvent(int Capacity, int MaxDailySessions, Guid GymId, Guid RoomId): IIntegrationEvent;