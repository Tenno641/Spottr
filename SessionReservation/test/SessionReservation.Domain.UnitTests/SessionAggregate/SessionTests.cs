using ErrorOr;
using SessionReservation.Domain.Common.Interfaces;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.UnitTests.Common.Interfaces;
using SessionReservation.Domain.UnitTests.Common.Participants;
using SessionReservation.Domain.UnitTests.Common.Sessions;

namespace SessionReservation.Domain.UnitTests.SessionAggregate;

public class SessionTests
{
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
}