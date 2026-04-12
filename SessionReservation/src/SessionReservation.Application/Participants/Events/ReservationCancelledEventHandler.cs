using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.EventualConsistency;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate.Events;

namespace SessionReservation.Application.Participants.Events;

public class ReservationCancelledEventHandler: INotificationHandler<ReservationCancelledEvent>
{
    private readonly IParticipantRepository _participantRepository;
    
    public ReservationCancelledEventHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }
    
    public async Task Handle(ReservationCancelledEvent notification, CancellationToken cancellationToken)
    {
        Participant? participant = await _participantRepository.GetByIdAsync(notification.ParticipantId);
        
        if (participant is null)
            throw new EventualConsistencyException($"Participant with id {notification.ParticipantId} is not found");
        
        ErrorOr<Success> cancelSessionResult = participant.CancelSession(notification.Session);

        if (cancelSessionResult.IsError)
            throw new EventualConsistencyException("Failed to cancel session", cancelSessionResult.Errors);

        await _participantRepository.UpdateParticipantAsync(participant);
    }
}