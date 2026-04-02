using ErrorOr;
using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.TrainerAggregate;
using SessionReservation.Domain.UnitTests.Common.Sessions;
using SessionReservation.Domain.UnitTests.Common.Trainers;

namespace SessionReservation.Domain.UnitTests.TrainerAggregate;

public class TrainerTests
{
    [Fact]
    public void TeachSession_SessionsOverlap_ShouldFail()
    {
        // Arrange
        Trainer trainer = TrainerFactory.Create();
        Session session1 = SessionFactory.CreateSession();
        Session session2 = SessionFactory.CreateSession();
        
        // Act
        ErrorOr<Created> session1Result = trainer.TeachSession(session1);
        ErrorOr<Created> session2Result =  trainer.TeachSession(session2);

        // Assert
        Assert.False(session1Result.IsError);
        Assert.Equal(Result.Created, session1Result.Value);
        
        Assert.True(session2Result.IsError);
        Assert.Equal(ErrorType.Conflict, session2Result.FirstError.Type);
        Assert.Equal(TrainerErrors.CannotTeachOverlappingSessions, session2Result.FirstError);
    }
}