using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Auth;

namespace UserManagement.Application.Users.Commands.RegisterUser;
 public record RegisterUserCommand(string Name, string Email, string Password): IRequest<ErrorOr<AuthenticationResponse>>;