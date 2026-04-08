using ErrorOr;
using GymManagement.Domain.Common;
using GymManagement.Domain.GymAggregate.Events;
using GymManagement.Domain.Rooms;
using GymManagement.Domain.Trainers;

namespace GymManagement.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private List<Guid> _roomIds = [];
    private int _maxRooms;
    private List<Guid> _trainersIds = [];
    
    public string Name { get; private set; }
    public Guid SubscriptionId { get; private set; }

    public Gym(
        Guid subscriptionId,
        int maxRooms,
        string name,
        Guid? id = null) : base(id)
    {
        SubscriptionId = subscriptionId;
        Name = name;
        _maxRooms = maxRooms;
    }

    public ErrorOr<Created> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id))
            throw new Exception("Rooms is already added");
        
        if (_roomIds.Count >= _maxRooms)
            return GymErrors.CannotHaveMoreRooms;
        
        _roomIds.Add(room.Id);
        _domainEvents.Add(new RoomAddedEvent(room));

        return Result.Created;
    }

    public ErrorOr<Success> AddTrainer(Trainer trainer)
    {
        if (_trainersIds.Contains(trainer.Id))
            return Error.Conflict(description: "Trainer already added to the gym");
        
        _trainersIds.Add(trainer.Id);
        
        _domainEvents.Add(new TrainerAddedEvent(trainer));
        
        return Result.Success;
    }

    public ErrorOr<Deleted> RemoveRoom(Guid roomId)
    {
        bool isDeleted = _roomIds.Remove(roomId);
        
        if (!isDeleted) return Error.NotFound(description: "Room is not found");

        _domainEvents.Add(new RoomDeletedEvent(roomId));
        
        return Result.Deleted;
    }
    
    private Gym() { }
}