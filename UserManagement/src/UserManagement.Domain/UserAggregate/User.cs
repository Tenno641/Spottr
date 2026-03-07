using UserManagement.Domain.Common;

namespace UserManagement.Domain.UserAggregate;

public class User: AggregateRoot
{
    public string Name { get; }
    public string Email { get; }
    
    public Guid AdminId { get; }
    public Guid ParticipantId { get; }
    public Guid TrainerId { get; }

    public User(string name, string email, Guid? id = null) : base(id)
    {
        Email = email;
        Name = name;
    }
}
