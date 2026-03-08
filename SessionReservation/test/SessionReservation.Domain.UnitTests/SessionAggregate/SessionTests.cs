using System.Runtime.CompilerServices;
using ErrorOr;
using SessionReservation.Domain.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.UnitTests.Common.Interfaces;
using SessionReservation.Domain.UnitTests.Common.Participants;
using SessionReservation.Domain.UnitTests.Common.Sessions;
using Xunit.Abstractions;

namespace SessionReservation.Domain.UnitTests.SessionAggregate;

public class SessionTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public SessionTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void CancelReservation_LessThan24Hour_ShouldFail()
    {
        // Arrange
        Participant participant = ParticipantFactory.CreateParticipant();
        Session session = SessionFactory.CreateSession();
        IDateTimeProvider dateTimeProvider = new DateTimeProvider(session.Date.ToDateTime(TimeOnly.MinValue));
        
        // Act
        session.ReserveSpot(participant);
        ErrorOr<Deleted> result = session.CancelReservation(participant.Id, dateTimeProvider);

        // Assert
        Assert.Equal(ErrorType.Forbidden, result.FirstError.Type);
    }

    [Fact]
    public void ReserveSpot_ParticipantSessionsConflict_ShouldFail()
    {
        // Arrange
        Session session = SessionFactory.CreateSession();
        Participant participant = ParticipantFactory.CreateParticipant();

        // Act
        ErrorOr<Success> result1 = participant.AddToSchedule(session);
        ErrorOr<Success> result2 = participant.AddToSchedule(session);
        
        // Assert
        Assert.Equal(Result.Success, result1.Value);
        Assert.False(result1.IsError);
        
        Assert.Equal(ErrorType.Conflict, result2.FirstError.Type);
        Assert.True(result2.IsError);
        Assert.Equal(result2.FirstError, ParticipantErrors.ParticipantHasOverlappingSessions);
    }
}