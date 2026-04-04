using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Auth;

namespace UserManagement.Application.Users.Commands.Login;

public record LoginCommand(string Email, string Password): IRequest<ErrorOr<AuthenticationResponse>>;