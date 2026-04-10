using ErrorOr;
using MediatR;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Sessions.Queries.GetSession;

public record GetSessionQuery(Guid SessionId): IRequest<ErrorOr<Session>>;