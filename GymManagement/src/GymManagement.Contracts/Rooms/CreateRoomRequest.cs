namespace GymManagement.Contracts.Rooms;

public record CreateRoomRequest(int MaxDailySessions, int Capacity);