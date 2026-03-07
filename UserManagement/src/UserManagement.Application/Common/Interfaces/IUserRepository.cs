using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Common.Interfaces;

public interface IUserRepository
{
    Task CreateAsync(User user, string password);
}