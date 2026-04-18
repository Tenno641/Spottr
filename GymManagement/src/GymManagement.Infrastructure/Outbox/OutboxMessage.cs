using System.Reflection;

namespace GymManagement.Infrastructure.Outbox;

public class OutboxMessage
{
    public int MessageId { get; }
    public DateTime OccurredOn { get; }
    public bool IsProcessed { get; set; }
    public string Type { get; }
    public string Issuer { get; }
    public string Body { get; }

    public OutboxMessage(string type, string body)
    {
        OccurredOn = DateTime.UtcNow;
        IsProcessed = false;
        Issuer = Assembly.GetExecutingAssembly().GetName().Name;
        Type = type;
        Body = body;
    }
}