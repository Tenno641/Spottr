namespace UserManagement.Domain;

public class User
{
    public Guid Id { get; }
    public string? Name { get; }
    
    public Guid AdminId { get; }
    public Guid ParticipantId { get; }
    public Guid TrainerId { get; }
}
