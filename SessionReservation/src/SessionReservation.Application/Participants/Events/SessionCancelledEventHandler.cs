using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.EventualConsistency;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.RoomAggregate.Events;

namespace SessionReservation.Application.Participants.Events;

public class SessionCancelledEventHandler: INotificationHandler<SessionCancelledEvent>
{
    private readonly IParticipantRepository _participantRepository;
    
    public SessionCancelledEventHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }

    public async Task Handle(SessionCancelledEvent notification, CancellationToken cancellationToken)
    {
        List<Participant> participants = await _participantRepository.GetParticipantsBySessionAsync(notification.Session.Id);

        foreach (Participant participant in participants)
        {
            ErrorOr<Success> result = participant.CancelSession(notification.Session);

            if (result.IsError)
                throw new EventualConsistencyException($"Failed to cancel session for participant {participant.Id}");
        }
        
        await _participantRepository.UpdateParticipantsAsync(participants);
    }
}