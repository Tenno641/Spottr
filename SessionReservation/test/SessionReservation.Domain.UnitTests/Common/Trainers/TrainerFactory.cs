using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Trainers;

public static class TrainerFactory
{
    public static Trainer Create(
        Guid? userId = null,
        string? name = null,
        Guid? id = null)
    {
        Trainer trainer = new Trainer(
            id: id ?? Constants.Constants.Trainers.Id,
            name: name ?? Constants.Constants.Trainers.Name,
            userId : userId ?? Constants.Constants.Trainers.UserId);

        return trainer;
    }
}