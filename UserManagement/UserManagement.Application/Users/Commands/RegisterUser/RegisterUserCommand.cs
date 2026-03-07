using ErrorOr;
using MediatR;

namespace UserManagement.Application.Users.Commands.RegisterUser;
 public record RegisterUserCommand(string FullName, string Email, string Password): IRequest<ErrorOr<Guid>>;