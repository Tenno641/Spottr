using UserManagement.Domain.UnitTests.Common.Users;
using UserManagement.Domain.UserAggregate;
using ErrorOr;

namespace UserManagement.Domain.UnitTests.UserAggregate;

public class UserTests
{
    [Fact]
    public void CreateParticipantProfile_UserDoesNotHaveParticipantProfile_ShouldReturnNewParticipantId()
    {
        // Arrange
        User user = UserFactory.CreateUser();
        
        // Act
        ErrorOr<Guid> result = user.CreateParticipantProfile();

        // Assert
        Assert.False(result.IsError);
    }

    [Fact]
    public void CreateParticipantProfile_UserAlreadyHasParticipantProfile_ShouldFail()
    {
        // Arrange
        User user = UserFactory.CreateUser();

        // Act
        ErrorOr<Guid> result1 = user.CreateParticipantProfile();
        ErrorOr<Guid> result2 = user.CreateParticipantProfile();

        // Assert
        Assert.False(result1.IsError);
        Assert.True(result2.IsError);
        Assert.Equal(ErrorType.Conflict, result2.FirstError.Type);
    }
}