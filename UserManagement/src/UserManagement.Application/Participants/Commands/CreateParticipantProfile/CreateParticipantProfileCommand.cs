using ErrorOr;
using MediatR;

namespace UserManagement.Application.Participants.Commands.CreateParticipantProfile;

public record CreateParticipantProfileCommand(Guid UserId): IRequest<ErrorOr<Guid>>;