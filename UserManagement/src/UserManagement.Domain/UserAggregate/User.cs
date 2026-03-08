using ErrorOr;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.UserAggregate;

public class User: AggregateRoot
{
    public string Name { get; }
    public string Email { get; }
    
    public Guid? AdminId { get; private set; }
    public Guid? ParticipantId { get; private set; }
    public Guid? TrainerId { get; private set; }
    
    private string _hashedPassword;

    public User(string name, string email, string hashedPassword, Guid? id = null) : base(id)
    {
        Email = email;
        _hashedPassword = hashedPassword;
        Name = name;
    }

    public ErrorOr<Guid> CreateAdminProfile()
    {
        if (AdminId is not null)
            return Error.Conflict(code: "User.CreateAdminProfile", description: "User already has admin profile");

        AdminId = Guid.CreateVersion7();
        
        // Todo: AdminProfileCreatedEvent

        return AdminId.Value;
    }
    
    public ErrorOr<Guid> CreateParticipantProfile()
    {
        if (ParticipantId is not null)
            return Error.Conflict(code: "User.CreateParticipantProfile", description: "User already has participant profile");

        ParticipantId = Guid.CreateVersion7();
        
        // Todo: ParticipantProfileCreatedEvent

        return ParticipantId.Value;
    }
    
    public ErrorOr<Guid> CreateTrainerProfile()
    {
        if (TrainerId is not null)
            return Error.Conflict(code: "User.CreateTrainerProfile", description: "User already has trainer profile");

        TrainerId = Guid.CreateVersion7();
        
        // Todo: TrainerProfileCreatedEvent

        return TrainerId.Value;
    }
}
