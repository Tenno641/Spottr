using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.CreateRoom;

public class CreateRoomCommandHandler: IRequestHandler<CreateRoomCommand, ErrorOr<Guid>>
{
    private readonly IGymsRepository _gymsRepository;
    
    public CreateRoomCommandHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetGymByIdAsync(request.GymId);
        if (gym is null)
            return Error.NotFound(description: "Gym is not found");

        Room room = new Room(request.MaxDailySessions, request.Capacity, request.GymId);

        ErrorOr<Created> result = gym.AddRoom(room);
        if (result.IsError)
            return result.Errors;

        await _gymsRepository.UpdateGymAsync(gym);

        return gym.Id;
    }
}