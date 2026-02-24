namespace Spottr.Domain;

public class Subscription
{
    private readonly Guid _id;
    private readonly Guid _adminId;
    private readonly List<Guid> _gymIds;
}