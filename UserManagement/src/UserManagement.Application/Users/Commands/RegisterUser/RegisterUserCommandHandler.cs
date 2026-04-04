using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Auth;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Common.Interfaces;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasher;
    
    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<ErrorOr<AuthenticationResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        string hashedPassword = _passwordHasher.HashPassword(request.Password);

        User user = new User(request.Name, request.Email, hashedPassword);

        await _userRepository.CreateAsync(user);

        AuthenticationResponse response = new AuthenticationResponse(Id: user.Id, "");

        return response;
    }
}