using ErrorOr;
using MediatR;
using UserManagement.Application.Common.Auth;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Common.Interfaces;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Application.Users.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    
    public LoginCommandHandler(IUserRepository userRepository, IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
    }
    
    public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return AuthenticationErrors.UserNotFound;

        bool isUserVerified = user.VerifyPassword(request.Password, _passwordHasherService);

        if (!isUserVerified)
            return AuthenticationErrors.UserIsNotAuthorized;

        AuthenticationResponse response = new AuthenticationResponse(Id: user.Id, "");

        return response;
    }
}