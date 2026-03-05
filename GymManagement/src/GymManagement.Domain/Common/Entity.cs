namespace GymManagement.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; }

    protected Entity(Guid? id = null)
    {
        Id = id ?? Guid.CreateVersion7();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    private Entity() { }
}