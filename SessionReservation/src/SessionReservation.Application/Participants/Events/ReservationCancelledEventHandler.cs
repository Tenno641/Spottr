using ErrorOr;
using MediatR;
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
        
        if (participant == null)
            return;
            // TODO: Eventual Consistency Exception

        ErrorOr<Success> cancelSessionResult = participant.CancelSession(notification.Session);

        if (cancelSessionResult.IsError)
            return;
        // TODO: Evential Consistency Exception
        

        await _participantRepository.UpdateParticipantAsync(participant);
    }
}