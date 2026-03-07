using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;
    
    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User(request.FullName, request.Email);

        await _userRepository.CreateAsync(user, request.Password);

        return user.Id;
    }
}