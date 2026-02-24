namespace Spottr.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    private readonly int _maxParticipants;

    public Session(
        Guid trainerId,
        int maxParticipants,
        Guid? id = null)
    {
        _trainerId = trainerId;
        _maxParticipants = maxParticipants;
        _id = id ?? Guid.CreateVersion7();
    }

    public void Reserve(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            throw new ArgumentOutOfRangeException();
        
        _participantIds.Add(participant.Id);
    }
}