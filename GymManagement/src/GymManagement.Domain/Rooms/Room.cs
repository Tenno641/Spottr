using GymManagement.Domain.Common;

namespace GymManagement.Domain.Rooms;

public class Room: Entity
{
    public int MaxDailySessions { get; }
    public int Capacity { get; }
    
    public Guid GymId { get; }

    public Room(
        int maxDailySessions,
        int capacity,
        Guid gymId,
        Guid? id = null) : base(id)
    {
        MaxDailySessions = maxDailySessions;
        Capacity = capacity;
        GymId = gymId;
    }
}