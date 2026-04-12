using ErrorOr;
using MediatR;
using SessionReservation.Application.Common.EventualConsistency;
using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.RoomAggregate.Events;

namespace SessionReservation.Application.Equipments.Events;

public class SessionCancelledEventHandler: INotificationHandler<SessionCancelledEvent>
{
    private readonly IEquipmentsRepository  _equipmentsRepository;
    
    public SessionCancelledEventHandler(IEquipmentsRepository equipmentsRepository)
    {
        _equipmentsRepository = equipmentsRepository;
    }

    public async Task Handle(SessionCancelledEvent notification, CancellationToken cancellationToken)
    {
        List<Guid> equipmentsIds = notification.Session.Equipments.Select(e => e.Id).ToList();
        
        List<Equipment> equipments = await _equipmentsRepository.GetEquipmentsByIds(equipmentsIds);

        foreach (Equipment equipment in equipments)
        {
            ErrorOr<Success> result = equipment.RemoveFromSchedule(notification.Session);
            
            if (result.IsError)
                throw new EventualConsistencyException($"Failed to clear equipment {equipment.Id} schedule");
        }

        await _equipmentsRepository.UpdateEquipmentsAsync(equipments);
    }
}