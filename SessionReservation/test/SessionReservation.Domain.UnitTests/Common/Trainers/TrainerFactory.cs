using SessionReservation.Domain.TrainerAggregate;

namespace SessionReservation.Domain.UnitTests.Common.Trainers;

public static class TrainerFactory
{
    public static Trainer Create()
    {
        Trainer trainer = new Trainer(
            id: Constants.Constants.Trainers.Id);

        return trainer;
    }
}