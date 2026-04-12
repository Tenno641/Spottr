using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Auth;

namespace UserManagement.Application.Users.Commands.RegisterUser;
 public record RegisterUserCommand(string Name, string Email, int Age, string Password): IRequest<ErrorOr<AuthenticationResponse>>;