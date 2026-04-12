namespace UserManagement.Contracts.Users;

public record RegisterUserRequest(string Name, string Email, int Age, string Password);