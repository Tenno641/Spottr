using ErrorOr;
using MediatR;

namespace GymManagement.Application.Rooms.CreateRoom;

public class CreateRoomCommandHandler: IRequestHandler<CreateRoomCommand, ErrorOr<Guid>>
{
    
    public Task<ErrorOr<Guid>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        
    }
}