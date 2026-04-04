using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByEmailAsync(string email);
    Task UpdateUserAsync(User user);
    Task CreateAsync(User user);
}