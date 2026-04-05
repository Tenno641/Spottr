using GymManagement.Domain.Common;

namespace GymManagement.Domain.Rooms;

public class Room: Entity
{
    private int _maxDailySessions;
    private int _capacity;
    
    public Guid GymId { get; }

    public Room(
        int maxDailySessions,
        int capacity,
        Guid gymId,
        Guid? id = null) : base(id)
    {
        _maxDailySessions = maxDailySessions;
        _capacity = capacity;
        GymId = gymId;
    }
}