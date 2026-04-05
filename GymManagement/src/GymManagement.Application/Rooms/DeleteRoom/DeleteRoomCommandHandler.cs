using ErrorOr;
using GymManagement.Application.Common.Interface;
using GymManagement.Domain.GymAggregate;
using MediatR;

namespace GymManagement.Application.Rooms.DeleteRoom;

public class DeleteRoomCommandHandler: IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
{
    private readonly IGymsRepository _gymsRepository;
    
    public DeleteRoomCommandHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }
    
    public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        Gym? gym = await _gymsRepository.GetGymByIdAsync(request.GymId);
        if (gym is null)
            return Error.NotFound(description: "Gym is not found");

        ErrorOr<Deleted> result = gym.RemoveRoom(request.RoomId);
        if (result.IsError)
            return result.Errors;

        await _gymsRepository.UpdateGymAsync(gym);

        return Result.Deleted;
    }
}