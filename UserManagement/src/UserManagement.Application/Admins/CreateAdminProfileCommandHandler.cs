using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Admins;

public class CreateAdminProfileCommandHandler: IRequestHandler<CreateAdminProfileCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;
    
    public CreateAdminProfileCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Error.NotFound(description: "User is not found");

        ErrorOr<Guid> result = user.CreateAdminProfile();

        if (result.IsError)
            return result.Errors;
        
        await _userRepository.UpdateUserAsync(user);
        
        return result.Value;
    }
}