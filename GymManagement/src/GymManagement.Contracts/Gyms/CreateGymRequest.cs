namespace GymManagement.Contracts.Gyms;

public record CreateGymRequest(string Name, List<string> Equipments);