using GymManagement.Domain.Common;

namespace GymManagement.Domain.Rooms;

public class Room: Entity
{
    public int MaxDailySessions { get; }
    public int Capacity { get; }
    public string Name { get; }
    public Guid GymId { get; }

    public Room(
        int maxDailySessions,
        int capacity,
        string name,
        Guid gymId,
        Guid? id = null) : base(id)
    {
        MaxDailySessions = maxDailySessions;
        Capacity = capacity;
        Name = name;
        GymId = gymId;
    }
}