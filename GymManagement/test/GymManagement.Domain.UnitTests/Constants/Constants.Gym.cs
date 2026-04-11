using GymManagement.Domain.Common.Entities;

namespace GymManagement.Domain.UnitTests.Constants;

public static partial class Constants
{
    public static class Gym
    {
        public static Guid Id => Guid.CreateVersion7();
        public static Guid SubscriptionId => Guid.CreateVersion7();
        public static string Name => "Gym-Name";
        public static int MaxRooms => 3;
        public static List<Equipment> Equipments => CreateEquipments();
    }

    private static List<Equipment> CreateEquipments()
    {
        Equipment equipment1 = new Equipment("Equipment-Name-1", id: Guid.CreateVersion7());
        Equipment equipment2 = new Equipment("Equipment-Name-2", id: Guid.CreateVersion7());
        Equipment equipment3 = new Equipment("Equipment-Name-3", id: Guid.CreateVersion7());
        
        return [equipment1,  equipment2, equipment3];
    }
}