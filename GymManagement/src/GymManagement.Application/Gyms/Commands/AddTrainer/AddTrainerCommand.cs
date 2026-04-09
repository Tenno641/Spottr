using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.AddTrainer;

public record AddTrainerCommand(Guid GymId, Guid TrainerId): IRequest<ErrorOr<Success>>;