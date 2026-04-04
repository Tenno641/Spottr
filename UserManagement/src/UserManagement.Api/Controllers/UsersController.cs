using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Common.Auth;
using UserManagement.Application.Participants.Commands.CreateParticipantProfile;
using UserManagement.Application.Users.Commands.Login;
using UserManagement.Application.Users.Commands.RegisterUser;
using UserManagement.Contracts.Participants;
using UserManagement.Contracts.Users;

namespace UserManagement.Api.Controllers;

public class UsersController: ApiController
{
    private readonly ISender _mediator;
    
    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        RegisterUserCommand command = new RegisterUserCommand(request.Name, request.Email, request.Password);

        ErrorOr<AuthenticationResponse> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.FirstError.Description)
            : Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginCommand command = new LoginCommand(request.Email, request.Password);

        ErrorOr<AuthenticationResponse> result = await _mediator.Send(command);

        if (result is { IsError: true, FirstError.Type: ErrorType.Unauthorized })
            return Problem(detail: result.FirstError.Description,
                statusCode: StatusCodes.Status401Unauthorized);

        return result.IsError
            ? Problem(result.Errors)
            : Ok(result.Value);
    }

    [HttpPost("create-participant")]
    public async Task<IActionResult> CreateParticipantProfile(CreateParticipantProfileRequest request)
    {
        CreateParticipantProfileCommand command = new CreateParticipantProfileCommand(request.UserId);

        ErrorOr<Guid> result = await _mediator.Send(command);

        return result.IsError
            ? Problem(result.FirstError.Description)
            : Ok(result.Value);
    }
}