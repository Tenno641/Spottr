using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate.Events;

namespace SessionReservation.Application.Participants.Events;

public class SessionSpotReservedEventHandler: INotificationHandler<SessionSpotReservedEvent>
{
    private readonly IParticipantRepository _participantRepository;
    
    public SessionSpotReservedEventHandler(IParticipantRepository participantRepository)
    {
        _participantRepository = participantRepository;
    }
    
    public async Task Handle(SessionSpotReservedEvent notification, CancellationToken cancellationToken)
    {
        Participant? participant = await _participantRepository.GetByIdAsync(notification.ParticipantId);

        if (participant is null)
            throw new ArgumentNullException();

        ErrorOr<Success> result = participant.AddToSchedule(notification.Session);

        if (result.IsError)
        {
            // retries or return exception for now.
        }

        await _participantRepository.UpdateParticipant(participant);
    }
}