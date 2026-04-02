using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate.Events;

namespace SessionReservation.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private Guid _trainerId;
    private List<Reservation> _reservations = [];
    private readonly SessionTypes _type;
    private readonly int _minimumAge;

    public DateOnly Date { get; }
    public TimeRange TimeRange { get; }
    
    public int Capacity { get; }

    public Session(Guid trainerId,
        int? capacity,
        SessionTypes type,
        DateOnly date,
        TimeRange timeRange,
        int minimumAge = int.MaxValue,
        Guid? id = null) : base(id)
    {
        _trainerId = trainerId;
        Capacity = capacity ?? GetCapacityByType();
        _type = type;
        Date = date;
        _minimumAge = minimumAge;
        TimeRange = timeRange;
    }

    public ErrorOr<Created> ReserveSpot(Participant participant)
    {
        if (IsSpotAlreadyReserved(participant.Id))
            return SessionErrors.SessionAlreadyReserved;

        if (participant.Age < _minimumAge)
            return SessionErrors.ParticipantMustMeetTheMinimumAge;

        Reservation reservation = new Reservation(participant.Id);
        
        _reservations.Add(reservation);

        _domainEvents.Add(new SessionSpotReservedEvent(this, participant.Id));

        return Result.Created;
    }

    public ErrorOr<Deleted> CancelReservation(Guid participantId, IDateTimeProvider dateTimeProvider)
    {
        if (IsCancellationTimeClose(dateTimeProvider))
            return Error.Forbidden(code: "Session.CancelReservation", description: "Cannot cancel reservation in the last 24 hour");

        _reservations.RemoveAll(reservation => reservation.ParticipantId == participantId);

        return Result.Deleted;
    }

    private bool IsCancellationTimeClose(IDateTimeProvider dateTimeProvider)
    {
        return (Date.ToDateTime(TimeRange.Start) - dateTimeProvider.UtcNow).TotalHours < 24;
    }

    private bool IsSpotAlreadyReserved(Guid participantId)
    {
        return _reservations.Any(reservation => reservation.ParticipantId == participantId);
    }

    private int GetCapacityByType()
    {
        return _type switch
        {
            SessionTypes.Cardio => 15,
            SessionTypes.Strength => 7,
            SessionTypes.Functional => 5,
            _ => 20
        };
    }
}