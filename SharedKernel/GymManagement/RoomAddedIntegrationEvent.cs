namespace SharedKernel.GymManagement;

public record RoomAddedIntegrationEvent(int Capacity, int MaxDailySessions, string Name, Guid GymId, Guid RoomId): IIntegrationEvent;