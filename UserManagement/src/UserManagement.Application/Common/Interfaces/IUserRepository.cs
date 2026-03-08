using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    void UpdateUser(User user);
    Task CreateAsync(User user);
}