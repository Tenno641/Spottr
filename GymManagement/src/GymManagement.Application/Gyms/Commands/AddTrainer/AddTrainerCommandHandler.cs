using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler: IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    private readonly IGymsRepository _gymsRepository;
    
    public AddTrainerCommandHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }
    
    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetGymByIdAsync(request.GymId);
        
        if (gym is null)
            return Error.NotFound(description: "Gym is not found");

        ErrorOr<Success> result = gym.AddTrainer(request.TrainerId);
        
        return result.IsError
            ? result.Errors
            : result.Value;
    }
}