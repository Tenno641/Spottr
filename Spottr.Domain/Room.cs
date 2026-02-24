namespace Spottr.Domain;

public class Room
{
    private readonly Guid _id;
    private readonly Guid _gymId;
    private readonly List<Guid> _sessionIds;
}