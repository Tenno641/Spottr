using Spottr.Domain.UnitTests.Common.Participants;
using Spottr.Domain.UnitTests.Common.Sessions;

namespace Spottr.Domain.UnitTests;

public class SessionTests
{
    [Fact]
    public void Reserve_WhenNoMorePlace_ShouldFail()
    {
        Session session = SessionFactory.CreateSession(1);

        Participant participant1 = ParticipantFactory.CreateParticipant();
        Participant participant2 = ParticipantFactory.CreateParticipant();

        session.Reserve(participant1);

        Action action = () => session.Reserve(participant2);

        Assert.Throws<ArgumentOutOfRangeException>(action);

    }
}