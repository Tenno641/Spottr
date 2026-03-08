using UserManagement.Application.Common.Interfaces;

namespace UserManagement.Infrastructure.Persistence.Repositories;

public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}