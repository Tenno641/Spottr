using ErrorOr;
using UserManagement.Domain.Common;
using UserManagement.Domain.Common.Interfaces;
using UserManagement.Domain.UserAggregate.Events;

namespace UserManagement.Domain.UserAggregate;

public class User: AggregateRoot
{
    public string Name { get; }
    public string Email { get; }
    public int Age { get; }
    
    public Guid? AdminId { get; private set; }
    public Guid? ParticipantId { get; private set; }
    public Guid? TrainerId { get; private set; }
    
    private string _hashedPassword;

    public User(
        string name, 
        string email, 
        string hashedPassword, 
        int age,
        Guid? adminId = null,
        Guid? participantId = null,
        Guid? trainerId = null,
        Guid? id = null) : base(id)
    {
        Email = email;
        _hashedPassword = hashedPassword;
        Name = name;
        Age = age;
        AdminId = adminId;
        ParticipantId = participantId;
        TrainerId = trainerId;
    }

    public bool VerifyPassword(string password, IPasswordHasherService passwordHasherService)
    {
        return passwordHasherService.VerifyPassword(password, _hashedPassword);
    }

    public ErrorOr<Guid> CreateAdminProfile()
    {
        if (AdminId is not null)
            return Error.Conflict(code: "User.CreateAdminProfile", description: "User already has admin profile");

        AdminId = Guid.CreateVersion7();
        
        _domainEvents.Add(new AdminProfileCreatedEvent(Id, AdminId.Value));

        return AdminId.Value;
    }
    
    public ErrorOr<Guid> CreateParticipantProfile()
    {
        if (ParticipantId is not null)
            return Error.Conflict(code: "User.CreateParticipantProfile", description: "User already has participant profile");

        ParticipantId = Guid.CreateVersion7();
        
        _domainEvents.Add(new ParticipantProfileCreatedEvent(Id, ParticipantId.Value, Name, Age));

        return ParticipantId.Value;
    }
    
    public ErrorOr<Guid> CreateTrainerProfile()
    {
        if (TrainerId is not null)
            return Error.Conflict(code: "User.CreateTrainerProfile", description: "User already has trainer profile");

        TrainerId = Guid.CreateVersion7();
        
        _domainEvents.Add(new TrainerProfileCreatedEvent(Id, TrainerId.Value, Name));

        return TrainerId.Value;
    }
}
