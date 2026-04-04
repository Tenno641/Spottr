namespace UserManagement.Application.Common.Auth;
using ErrorOr;

public static class AuthenticationErrors
{
    public static Error UserNotFound => Error.NotFound("GetUserByEmail", "User is not found");
    public static Error UserIsNotAuthorized => Error.Unauthorized("User.Login", "Invalid Password or Email");
}