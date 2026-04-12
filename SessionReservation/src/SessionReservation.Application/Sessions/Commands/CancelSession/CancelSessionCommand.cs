using ErrorOr;
using MediatR;

namespace SessionReservation.Application.Sessions.Commands.CancelSession;

public record CancelSessionCommand(Guid RoomId,Guid SessionId) : IRequest<ErrorOr<Success>>;