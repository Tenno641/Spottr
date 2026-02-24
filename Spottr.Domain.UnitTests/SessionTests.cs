using Spottr.Domain.Services;
using Spottr.Domain.UnitTests.Common;
using Spottr.Domain.UnitTests.Common.Participants;
using Spottr.Domain.UnitTests.Common.Sessions;

namespace Spottr.Domain.UnitTests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMorePlace_ShouldFail()
    {
        // Arrange
        Session session = SessionFactory.CreateSession(1);

        Participant participant1 = ParticipantFactory.CreateParticipant();
        Participant participant2 = ParticipantFactory.CreateParticipant();

        // Act
        session.ReserveSpot(participant1);

        // Assert
        Action action = () => session.ReserveSpot(participant2);
        Assert.Throws<ArgumentOutOfRangeException>(action);
    }

    [Fact]
    public void CancelReservation_WhenItIsCloseToTheSession_ShouldFail()
    {
        // Arrange
        Session session = SessionFactory.CreateSession(1);

        Participant participant = ParticipantFactory.CreateParticipant();

        IDateTimeProvider dateTimeProvider = new DateTimeProvider(Constants.Constants.Session.Date.ToDateTime(TimeOnly.MinValue));
        
        // Act
        Action action = () => session.CancelReservation(participant, dateTimeProvider);
        
        // Assert
        Assert.Throws<Exception>(action);
    }
}