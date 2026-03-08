using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Participants.Commands.CreateParticipantProfile;

public class CreateParticipantProfilerCommandHandler: IRequestHandler<CreateParticipantProfileCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;
    
    public CreateParticipantProfilerCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateParticipantProfileCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Error.NotFound(description: "User is not found");

        ErrorOr<Guid> result = user.CreateParticipantProfile();

        _userRepository.UpdateUser(user);

        return result;
    }
}