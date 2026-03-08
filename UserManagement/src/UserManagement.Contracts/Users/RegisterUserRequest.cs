namespace UserManagement.Contracts.Users;

public record RegisterUserRequest(string Name, string Email, string Password);