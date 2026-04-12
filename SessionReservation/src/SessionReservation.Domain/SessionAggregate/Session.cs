using ErrorOr;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.Common.Interfaces;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate.Events;

namespace SessionReservation.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private List<Reservation> _reservations = [];

    public List<Equipment> Equipments { get; }
    public Guid TrainerId { get; private set; }
    public Guid RoomId { get; private set; }
    public int MinimumAge { get; }
    public SessionTypes Type { get; }
    public DateOnly Date { get; }
    public TimeRange TimeRange { get; }
    public int Capacity { get; }

    public Session(Guid trainerId,
        Guid roomId,
        int? capacity,
        SessionTypes type,
        DateOnly date,
        TimeRange timeRange,
        List<Equipment> equipments,
        int minimumAge = int.MaxValue,
        Guid? id = null) : base(id)
    {
        TrainerId = trainerId;
        RoomId = roomId;
        Capacity = capacity ?? GetCapacityByType();
        Type = type;
        Date = date;
        MinimumAge = minimumAge;
        Equipments = equipments;
        TimeRange = timeRange;
    }

    public ErrorOr<Created> ReserveSpot(Participant participant)
    {
        if (IsSpotAlreadyReserved(participant.Id))
            return SessionErrors.SessionAlreadyReserved;

        if (participant.Age < MinimumAge)
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

        Reservation? reservation = _reservations.FirstOrDefault(reservation => reservation.ParticipantId == participantId);
        if (reservation is null)
            return SessionErrors.ReservationNotFound;
        
        // Consider Paying pack TODO: Remember this
            
        _reservations.Remove(reservation);
        
        _domainEvents.Add(new ReservationCancelledEvent(this, participantId));

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
        return Type switch
        {
            SessionTypes.Cardio => 15,
            SessionTypes.Strength => 7,
            SessionTypes.Functional => 5,
            _ => 20
        };
    }
    
    private Session() { }
}