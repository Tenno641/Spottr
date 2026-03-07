using UserManagement.Domain;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Common.Interfaces;

public interface ITokenService
{
    Task<string> GenerateToken(User user);
}