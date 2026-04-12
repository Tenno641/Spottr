using ErrorOr;
using MediatR;

namespace UserManagement.Application.Admins;

public record CreateAdminProfileCommand(Guid UserId): IRequest<ErrorOr<Guid>>;