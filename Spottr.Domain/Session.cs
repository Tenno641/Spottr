using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Spottr.Domain.Services;

namespace Spottr.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private DateOnly _date;
    private TimeRange _timeRange;
    private readonly List<Guid> _participantIds = [];
    private readonly int _maxParticipants;

    public Session(
        Guid trainerId,
        int maxParticipants, 
        TimeRange timeRange,
        DateOnly date, 
        Guid? id = null)
    {
        _trainerId = trainerId;
        _maxParticipants = maxParticipants;
        _timeRange = timeRange;
        _date = date;
        _id = id ?? Guid.CreateVersion7();
    }

    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
            throw new ArgumentOutOfRangeException();
        
        _participantIds.Add(participant.Id);
    }
    public void CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
            throw new Exception("Too late to cancel the session");

        if (!_participantIds.Remove(participant.Id))
            throw new Exception("Error occurred cancelling the reservation");
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minimumHours = 24;
        
        return (_date.ToDateTime(_timeRange.StartTime) - utcNow).Hours >= minimumHours;
    }
}