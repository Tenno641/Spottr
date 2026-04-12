using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.EventualConsistency;
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
            throw new EventualConsistencyException($"Participant with id {notification.ParticipantId} is not found");

        ErrorOr<Success> result = participant.AddToSchedule(notification.Session);

        if (result.IsError)
            throw new EventualConsistencyException($"Failed to reserve spot for participant {participant.Id}", result.Errors);

        await _participantRepository.UpdateParticipantAsync(participant);
    }
}