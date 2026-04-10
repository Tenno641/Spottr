using MediatR;
using SessionReservation.Domain.SessionAggregate;
using ErrorOr;

namespace SessionReservation.Application.Sessions.Queries.ListSessions;

public record ListSessionsQuery(Guid GymId, DateTime? StartDateTime, DateTime? EndDateTime, List<SessionTypes>? Types): IRequest<ErrorOr<List<Session>>>;