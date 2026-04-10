using ErrorOr;
using MediatR;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Application.Participants.Queries.ListParticipantSessions;

public record ListParticipantSessionsQuery(Guid ParticipantId, DateTime? StartDateTime, DateTime? EndDateTime, List<SessionTypes>? Types): IRequest<ErrorOr<List<Session>>>;