using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Trainers;

public class CreateTrainerProfileCommandHandler: IRequestHandler<CreateTrainerProfileCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;
    
    public CreateTrainerProfileCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateTrainerProfileCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Error.NotFound(description: "User is not found");

        ErrorOr<Guid> result = user.CreateTrainerProfile();

        if (result.IsError)
            return result.Errors;

        await _userRepository.UpdateUserAsync(user);

        return result.Value;
    }
}