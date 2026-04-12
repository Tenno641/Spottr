using UserManagement.Domain.UserAggregate;

namespace UserManagement.Domain.UnitTests.Common.Users;

public static class UserFactory
{
    public static User CreateUser(
        Guid? id = null,
        int? age = null,
        string? name = null,
        string? email = null,
        string? hashedPassword = null)
    {
        User user = new User(
            id: id ?? Constants.Constants.User.Id,
            name: name ?? Constants.Constants.User.Name,
            age: age ?? Constants.Constants.User.Age,
            email: email ?? Constants.Constants.User.Email,
            hashedPassword: hashedPassword ?? Constants.Constants.User.HashedPassword);

        return user;
    }
}