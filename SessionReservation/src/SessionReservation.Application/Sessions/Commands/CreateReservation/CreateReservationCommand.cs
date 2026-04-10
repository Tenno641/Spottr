using ErrorOr;
using MediatR;

namespace SessionReservation.Application.Sessions.Commands.CreateReservation;

public record CreateReservationCommand(Guid ParticipantId, Guid SessionId): IRequest<ErrorOr<Created>>;