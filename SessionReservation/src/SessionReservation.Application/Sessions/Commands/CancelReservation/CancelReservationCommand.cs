using ErrorOr;
using MediatR;

namespace SessionReservation.Application.Sessions.Commands.CancelReservation;

public record CancelReservationCommand(Guid SessionId, Guid ParticipantId): IRequest<ErrorOr<Success>>;