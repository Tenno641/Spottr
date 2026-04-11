using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Sessions;

public static class SessionFactory
{
    public static Session CreateSession(
        Guid? trainerId = null,
        Guid? roomId = null,
        int? capacity = null,
        DateOnly? date = null,
        TimeRange? timeRange = null,
        int? minimumAge = null,
        List<Equipment>? equipments = null,
        Guid? id = null)
    {
        Session session = new Session(
            trainerId: trainerId ?? Constants.Constants.Trainers.Id,
            roomId: roomId ?? Constants.Constants.Sessions.RoomId,
            capacity: capacity ?? Constants.Constants.Sessions.Capacity,
            type: SessionTypes.Cardio,
            id: id ?? Constants.Constants.Sessions.Id,
            date: date ?? Constants.Constants.Sessions.Date,
            minimumAge: minimumAge ?? Constants.Constants.Sessions.MinimumAge,
            equipments: equipments ?? Constants.Constants.Sessions.RequiredEquipments,
            timeRange: timeRange ?? Constants.Constants.Sessions.TimeRange
        );

        return session;
    }
}