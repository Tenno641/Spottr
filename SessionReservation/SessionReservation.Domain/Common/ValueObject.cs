namespace SessionReservation.Domain.Common;

public abstract class ValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
            return false;

        return ((ValueObject)obj).GetProperties()
            .SequenceEqual(GetProperties());
    }

    public override int GetHashCode()
    {
        return GetProperties()
            .Select(property => property.GetHashCode())
            .Aggregate((x, y) => x ^ y);
    }

    protected abstract IEnumerable<object> GetProperties();
    
    private ValueObject() { }
}