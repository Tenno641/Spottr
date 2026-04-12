using ErrorOr;
using MediatR;

namespace UserManagement.Application.Trainers;

public record CreateTrainerProfileCommand(Guid UserId): IRequest<ErrorOr<Guid>>;