namespace UserManagement.Contracts.Users;

public record RegisterUserRequest(string FullName, string Email, string Password);